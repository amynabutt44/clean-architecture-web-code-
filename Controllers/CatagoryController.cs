using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using application; // Use the correct namespace for your service class
using domain.Models;

namespace WebApplication2.Controllers
{
	public class CatagoryController : Controller
	{
		private readonly IWebHostEnvironment _env; // For accessing the web root path
		private readonly catagoryservice _catagoryService; // Service for category operations

		// Constructor for dependency injection
		public CatagoryController(IWebHostEnvironment env, catagoryservice catagoryService)
		{
			_env = env;
			_catagoryService = catagoryService;
		}

		// Display a list of categories
		public ViewResult Index()
		{
			List<catagory> categories = _catagoryService.GetAll().ToList(); // Get all categories
			ViewBag.MyList = categories; // Pass categories to the view
			return View(categories); // Return the view with categories
		}

		// GET: Display the delete category view
		[HttpGet]
		public ViewResult Delete()
		{
			return View(); // Return the view for deleting a category
		}

		// POST: Delete a category
		[HttpPost]
		public ActionResult Delete(int code)
		{
			var catagoryToDelete = new catagory { Id = code }; // Create a category instance with the given ID
			_catagoryService.delete(catagoryToDelete); // Delete the category using the service
			return RedirectToAction("Index", "Catagory"); // Redirect to the Index action to refresh the category list
		}

		// GET: Display the update image view
		[HttpGet]
		public IActionResult UpdateImage()
		{
			return View(); // Return the view for updating category images
		}

		// POST: Update the image for a category
		[HttpPost]
		public IActionResult UpdateImage(int code, IFormFile image)
		{
			if (image == null || image.Length == 0)
			{
				ModelState.AddModelError("image", "Please select a file"); // Add model error if no file is selected
				return View(); // Return the view with the error message
			}

			// Define the path to save the uploaded image
			string wwwrootPath = _env.WebRootPath;
			string path = Path.Combine(wwwrootPath, "uploadedfiles");

			// Create directory if it does not exist
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			// Generate a unique file name
			string fileName = $"{code}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
			string filePath = Path.Combine(path, fileName);

			// Save the uploaded image to the server
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				image.CopyTo(fileStream); // Copy the uploaded image to the file stream
			}

			// Create a category instance with the new image file name
			var catagoryToUpdate = new catagory
			{
				Id = code,
				image = fileName
			};

			_catagoryService.update(catagoryToUpdate); // Update the category with the new image

			return RedirectToAction("Index", "Catagory"); // Redirect to the Index action to refresh the category list
		}

		// GET: Display the add category view
		[HttpGet]
		public ViewResult Add()
		{
			return View(); // Return the view for adding a new category
		}

		// POST: Add a new category
		[HttpPost]
		public IActionResult Add(catagory model, IFormFile image)
		{
			if (ModelState.IsValid)
			{
				// Define the path to save the uploaded image
				string wwwrootPath = _env.WebRootPath;
				string path = Path.Combine(wwwrootPath, "uploadedfiles");

				// Create directory if it does not exist
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				// Generate a unique file name for the image
				string fileName = Path.GetFileName(image.FileName); // Use the original file name
				string filePath = Path.Combine(path, fileName);

				// Save the uploaded image to the server
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					image.CopyTo(fileStream); // Copy the uploaded image to the file stream
				}

				model.image = fileName; // Set the image file name in the category model

				_catagoryService.Add(model); // Add the new category using the service

				return RedirectToAction("Index", "Catagory");
			}
			else
			{
				return View(); // Return the view with the validation errors
			}
		}
	}
}
