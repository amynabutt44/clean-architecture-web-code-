using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using domain.Models.viewmodel;
using application;
using domain;
// Import MVC models

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly productService _productService;
        private readonly catagoryservice _catagoryService;
        private readonly orderService _orderService;

        public HomeController(ILogger<HomeController> logger, productService productService, catagoryservice catagoryService, orderService orderService)
        {
            _logger = logger;
            _productService = productService;
            _catagoryService = catagoryService;
            _orderService = orderService;
        }

		public IActionResult Index()
		{
			var domainProducts = _productService.GetAll().ToList();
			var domainCategories = _catagoryService.GetAll().ToList();

			// Manual mapping from domain to MVC models
			var products = domainProducts.Select(p => new product
			{
				Id = p.Id,
				name = p.name,
				price = p.price,
				image = p.image // Include the image property
								// Add other properties as needed
			}).ToList();

			var categories = domainCategories.Select(c => new domain.Models.catagory
			{
				Id = c.Id,
				Name = c.Name,
				image = c.image // Include the image property
								// Add other properties as needed
			}).ToList();

			var viewModel = new Catagoryproduct
			{
				product = products,
				c = categories
			};

			ViewBag.MyList = categories;

			return View(viewModel);
		}

		public ViewResult aboutUs()
        {
            var domainCategories = _catagoryService.GetAll().ToList();
            var categories = domainCategories.Select(c => new   domain.Models.catagory
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            ViewBag.MyList = categories;

            return View();
        }

        public ViewResult refundpolicy()
        {
            var domainCategories = _catagoryService.GetAll().ToList();
            var categories = domainCategories.Select(c => new domain.Models.catagory
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            ViewBag.MyList = categories;

            return View();
        }
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                // If the search term is empty, return an empty list or default result
                return PartialView("_SearchResultsPartial", new List<product>());
            }

            var domainSearchResults = _productService.GetProductsByName(searchTerm).ToList();
            var searchResults = domainSearchResults.Select(p => new product
            {
                Id = p.Id,
                name = p.name,
                price = p.price,
                image = p.image
            }).ToList();

            // Return the partial view with search results
            return PartialView("_SearchResultsPartial", searchResults);
        }

        //[HttpGet]
        //public IActionResult Search(string searchTerm)
        //{
        //    if (string.IsNullOrEmpty(searchTerm))
        //    {
        //        return View("SearchResults", new List<product>());
        //    }

        //    var domainSearchResults = _productService.GetProductsByName(searchTerm).ToList();
        //    var searchResults = domainSearchResults.Select(p => new product
        //    {
        //        Id = p.Id,
        //        name = p.name,
        //        price = p.price,
        //        image = p.image
        //    }).ToList();

        //    return PartialView("_SearchResultsPartial", searchResults);
        //}


        public ViewResult contactUs()
        {
            var domainCategories = _catagoryService.GetAll().ToList();
            var categories = domainCategories.Select(c => new domain.Models.catagory
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            ViewBag.MyList = categories;

            return View();
        }

        public ViewResult FAQ()
        {
            var domainCategories = _catagoryService.GetAll().ToList();
            var categories = domainCategories.Select(c => new domain.Models.catagory
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            ViewBag.MyList = categories;

            return View();
        }

        public ViewResult Error()
        {
            return View();
        }
    }
}
