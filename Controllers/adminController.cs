using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using application; // Use the correct namespace for your service classes
using domain.Models;
using domain;

namespace WebApplication2.Controllers
{
	public class adminController : Controller
	{
		private readonly orderService _orderService; // Service for managing orders
		private readonly productService _productService; // Service for managing products
		private readonly catagoryservice _catagoryService; // Service for managing categories

		// Constructor for dependency injection
		public adminController(orderService orderService, catagoryservice catagoryService, productService productService)
		{
			_catagoryService = catagoryService;
			_productService = productService;
			_orderService = orderService;
		}

		// Display the admin dashboard (requires "adminonly" policy)
		[Authorize(Policy = "adminonly")]
		public IActionResult Index()
		{
			return View(); // Returns the default view for the admin dashboard
		}

		// Display a list of products (requires "adminonly" policy)
		[Authorize(Policy = "adminonly")]
		public IActionResult products()
		{
			List<product> products = _productService.GetAll().ToList(); // Get all products
			List<catagory> categories = _catagoryService.GetAll().ToList(); // Get all categories
			ViewBag.MyList = categories; // Pass categories to the view
			return View(products); // Pass products to the view
		}

		// Display a list of all orders
		public IActionResult allorders()
		{
			List<Orderr> orders = _orderService.GetAll().ToList(); // Get all orders
			List<catagory> categories = _catagoryService.GetAll().ToList(); // Get all categories
			ViewBag.MyList = categories; // Pass categories to the view
			return View(orders); // Pass orders to the view
		}
	}
}
