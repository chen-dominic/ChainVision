using ChainVisionApp.Models;
using ChainVision.Data.Models;
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

            var inventoryData = _cvContext.MaterialInventories.ToList();
            var latestTime = _cvContext.UpdatedTimes.Select(_ => _.LastUpdatedTimeUtc).OrderByDescending(_ => _).First();

            var data = new DashboardViewModel
            {
                CocoaData = cocoaData,
                SpringWheatData = springWheatData,
                SugarData = sugarData,
                WheatData = wheatData,
                AlertNewsData = alertData,
                Inventory = inventoryData,
                LatestUpdated = latestTime
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
