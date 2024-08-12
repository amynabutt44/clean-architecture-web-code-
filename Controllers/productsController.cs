using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using domain;
using domain.Models;
using Microsoft.AspNetCore.Http;
using application; // Import the application namespace where your services are located

namespace WebApplication2.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IWebHostEnvironment _env;
		private readonly productService _productService; // Service for product operations
		private readonly catagoryservice _catagoryService; // Service for category operations

		// Constructor for dependency injection
		public ProductsController(IWebHostEnvironment env, productService productService, catagoryservice catagoryService)
		{
			_env = env;
			_productService = productService;
			_catagoryService = catagoryService;
		}

		// Display the list of all products
		public IActionResult Index()
		{
			var products = _productService.GetAll().ToList(); // Get all products
			List<catagory> categories = _catagoryService.GetAll().ToList(); // Get all categories
			ViewBag.MyList = categories; // Pass categories to the view using ViewBag
			return View(products); // Return the view with products
		}

		// Display products based on the selected category name
		public IActionResult AllProduct(string name)
		{
			HttpContext.Response.Cookies.Delete("first_request"); // Remove cookie if it exists

			int id = _catagoryService.getid(name); // Get category ID based on name
			var products = _productService.GetById(id).ToList(); // Get products by category ID

			List<catagory> categories = _catagoryService.GetAll().ToList(); // Get all categories
			ViewBag.MyList = categories; // Pass categories to the view using ViewBag
			return View(products); // Return the view with products
		}

		// Display the view for adding a new product
		[HttpGet]
		public ViewResult Add()
		{
			List<catagory> categories = _catagoryService.GetAll().ToList();
			// Get all categories for the dropdown
			return View(categories); // Return the view for adding a product
		}

		// Handle the form submission for adding a new product
		[HttpPost]
		public IActionResult Add(string name, int price, string description, IFormFile image, int category)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Error", "Home"); // Redirect to error page if model state is invalid
			}

			string fileName = Path.GetFileName(image.FileName); // Get the image file name

			product newProduct = new product
			{
				name = name,
				price = price,
				c_id = category,
				description = description,
				image = fileName
			};

			_productService.Add(newProduct); // Add the new product using the service

			// Save the image file to the server
			string wwwrootPath = _env.WebRootPath;
			string path = Path.Combine(wwwrootPath, "uploadedfiles");

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path); // Create the directory if it doesn't exist
			}

			string filePath = Path.Combine(path, fileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				image.CopyTo(fileStream); // Save the image file to the server
			}

			return RedirectToAction("products", "admin"); // Redirect to the admin products page
		}

		// Handle the form submission for deleting a product
		[HttpPost]
		public ActionResult Delete(int id)
		{
			var productToDelete = new product { Id = id }; // Create a product object with the ID
			_productService.delete(productToDelete); // Delete the product using the service
			return RedirectToAction("products", "admin"); // Redirect to the admin products page
		}

		// Display the view for updating a product
		[HttpGet]
		public ViewResult Update()
		{
			return View(); // Return the view for updating a product
		}

		// Handle the form submission for updating a product's price and name
		[HttpPost]
		public IActionResult Update(int code, int price, string name)
		{
			product productToUpdate = new product
			{
				price = price,
				Id = code,
				name = name
			};

			_productService.update_price(productToUpdate); // Update the product's price using the service
			return RedirectToAction("products", "admin"); // Redirect to the admin products page
		}
	}
}
