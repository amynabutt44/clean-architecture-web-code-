using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

using application;
using domain.Models;
using domain;

namespace WebApplication2.Controllers
{
	public class AddToCartController : Controller
	{
		private const string CartSessionKey = "Cart";
		private readonly orderService _orderService;
		private readonly productService _productService;
		private readonly catagoryservice _catagoryService;

		public AddToCartController(orderService orderService, catagoryservice catagoryService, productService productService)
		{
			_catagoryService = catagoryService;
			_productService = productService;
			_orderService = orderService;
		}

		// Display the list of products
		public IActionResult Index()
		{
			var  products = _productService.GetAll().ToList();
			return View(products);
		}

		// Add a product to the cart
		[HttpPost]
		public IActionResult Cart(int productId)
		{
			var user = User;

			if (user.Identity?.IsAuthenticated == true)
			{
				var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

				if (userId != null)
				{
					HttpContext.Session.SetString("UserId", userId);
					TempData["userid"] = userId;

					HttpContext.Session.SetInt32("Id", productId);

					var product = _productService.GetAll().FirstOrDefault(p => p.Id == productId);
					if (product != null)
					{
						var cart = GetCart();
						var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId.ToString());

						if (cartItem != null)
						{
							cartItem.Quantity += 1; // Increase quantity if item already exists
						}
						else
						{
							cart.Add(new CartItem
							{
								ProductId = productId.ToString(),
								Quantity = 1,
								Price = product.price.ToString(),
								Name = product.name,
								Image = product.image
							});
						}

						SaveCart(cart); // Save updated cart

						return RedirectToAction("ViewCarts", "AddToCart");
					}
				}
			}
			return RedirectToPage("/Account/Login", new { area = "Identity" });
		}

		// Remove a product from the cart
		public IActionResult RemoveFromCart(int productId)
		{
			var cart = GetCart();
			var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId.ToString());

			if (cartItem != null)
			{
				cart.Remove(cartItem); // Remove item from cart
				SaveCart(cart); // Save updated cart
			}

			return RedirectToAction("ViewCarts");
		}

		// Increase the quantity of a product in the cart
		[HttpPost]
		public IActionResult IncreaseQuantity(int productId)
		{
			var cart = GetCart();
			var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId.ToString());

			if (cartItem != null)
			{
				cartItem.Quantity += 1; // Increase quantity
				SaveCart(cart); // Save updated cart
			}

			return RedirectToAction("ViewCarts");
		}

		// Decrease the quantity of a product in the cart
		[HttpPost]
		public IActionResult DecreaseQuantity(int productId)
		{
			var cart = GetCart();
			var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId.ToString());

			if (cartItem != null && cartItem.Quantity > 1)
			{
				cartItem.Quantity -= 1; // Decrease quantity if more than 1
				SaveCart(cart); // Save updated cart
			}

			return RedirectToAction("ViewCarts");
		}

		// Display the contents of the cart
		public IActionResult ViewCarts()
		{
			var cart = GetCart();

			ViewBag.TotalAmount = cart.Sum(item => item.Quantity * int.Parse(item.Price)); // Calculate total amount

			List<catagory> products = _catagoryService.GetAll().ToList();
			ViewBag.MyList = products;
			return View(cart);
		}

		// Get the cart from session
		private List<CartItem> GetCart()
		{
			var cartJson = HttpContext.Session.GetString(CartSessionKey);
			if (string.IsNullOrEmpty(cartJson))
			{
				return new List<CartItem>();
			}

			return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
		}

		// Save the cart to session
		private void SaveCart(List<CartItem> cart)
		{
			var cartJson = JsonSerializer.Serialize(cart);
			HttpContext.Session.SetString(CartSessionKey, cartJson);
		}

		// Display checkout page with cart details
		public IActionResult Checkout()
		{
			var cart = GetCart();
			var totalAmount = cart.Sum(item => item.Quantity * int.Parse(item.Price));

			ViewBag.TotalAmount = totalAmount;

			List<catagory> products = _catagoryService.GetAll().ToList();
			ViewBag.MyList = products;
			return View(cart);
		}

		// Process delivery information and create an order
		[HttpPost]
		public IActionResult DeliveryInformation(DeliveryViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("OrderSummary", GetCart()); // Return to summary if model is invalid
			}

			var userId = HttpContext.Session.GetString("UserId");

			if (userId == null)
			{
				return RedirectToPage("/Account/Login", new { area = "Identity" });
			}

			var cart = GetCart();
			var totalAmount = cart.Sum(item => item.Quantity * int.Parse(item.Price));

			var order = new Orderr
			{
				UserId = userId,
				ProductId = HttpContext.Session.GetInt32("Id") ?? 0,
				Address = model.Address,
				PhoneNumber = model.PhoneNumber,
				TotalAmount = totalAmount,
				OrderDate = DateTime.Now,
				Name = model.Name
			};

			_orderService.Add(order); // Add order to repository

			SaveCart(new List<CartItem>()); // Clear the cart

			return RedirectToAction("OrderConfirmation", new { userId = userId });
		}

		// Display order confirmation
		public IActionResult OrderConfirmation(string userId)
		{
			List<Orderr> orders = _orderService.GetOrders(userId);

			if (orders.Count == 0)
			{
				return NotFound();
			}

			Orderr mostRecentOrder = orders.OrderByDescending(o => o.OrderDate).FirstOrDefault();

			List<catagory> products = _catagoryService.GetAll().ToList();
			ViewBag.MyList = products;
			return View(mostRecentOrder);
		}
	}
}
