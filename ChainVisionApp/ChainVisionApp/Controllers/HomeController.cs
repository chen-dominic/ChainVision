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
            var cocoaData = _cvContext.IngredientsDataCocoas.ToList();
            var springWheatData = _cvContext.IngredientsDataSpringWheats.ToList();
            var sugarData = _cvContext.IngredientsDataSugars.ToList();
            var wheatData = _cvContext.IngredientsDataWheats.ToList();
            var alertData = _cvContext.VwRecentHighSeverityNews.ToList();

            var data = new DashboardViewModel
            {
                CocoaData = cocoaData,
                SpringWheatData = springWheatData,
                SugarData = sugarData,
                WheatData = wheatData,
                AlertNewsData = alertData,
            };

            return View(data);
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
