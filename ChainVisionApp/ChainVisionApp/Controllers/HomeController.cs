using ChainVisionApp.Models;
using ChainVisionApp.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChainVisionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChainVisionContext _cvContext;

        public HomeController(ILogger<HomeController> logger, ChainVisionContext cvContext)
        {
            _logger = logger;
            _cvContext = cvContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Products()
        {
            // Fetch the data from the view first
            var products = _cvContext.VwProductMaterialDetails
                .ToList()  // Materialize the query to a list
                .GroupBy(pd => new { pd.ProductId, pd.ProductName, pd.ImageUrl }) // Group by product information
                .Select(group => new ProductViewModel
                {
                    Id = group.Key.ProductId,
                    ProductName = group.Key.ProductName,
                    ImageUrl = group.Key.ImageUrl,
                    Ingredients = string.Join(", ", group.Select(pd => pd.MaterialName)) // Join all ingredients for the product
                })
                .OrderBy(_ => _.ProductName)
                .ToList(); // Execute the grouping and join operation in memory

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
