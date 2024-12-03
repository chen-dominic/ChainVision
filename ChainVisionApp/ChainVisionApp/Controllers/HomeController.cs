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
            var inventoryData = _cvContext.MaterialInventories.ToList();

            // Prepare the chart data for the cocoa, wheat, sugar, and spring wheat
            var cocoaChartData = cocoaData.Select(d => new MaterialDataViewModel
            {
                Date = d.Time.HasValue ? d.Time.Value.ToString("yyyy-MM") : "Unknown",
                Previous = d.Previous ?? 0
            }).OrderBy(_ => _.Date).ToList();

            var springWheatChartData = springWheatData.Select(d => new MaterialDataViewModel
            {
                Date = d.Time.HasValue ? d.Time.Value.ToString("yyyy-MM") : "Unknown",
                Previous = d.Previous ?? 0
            }).OrderBy(_ => _.Date).ToList();
            
            var sugarChartData = sugarData.Select(d => new MaterialDataViewModel
            {
                Date = d.Time.HasValue ? d.Time.Value.ToString("yyyy-MM") : "Unknown",
                Previous = d.Previous ?? 0
            }).OrderBy(_ => _.Date).ToList();
            
            var wheatChartData = wheatData.Select(d => new MaterialDataViewModel
            {
                Date = d.Time.HasValue ? d.Time.Value.ToString("yyyy-MM") : "Unknown",
                Previous = d.Previous ?? 0
            }).OrderBy(_ => _.Date).ToList();

            var inventoryChartData = inventoryData
                .GroupBy(i => i.Ingredient)
                .ToDictionary(
                    g => g.Key,
                    g => new InventoryChartData
                    {
                        Labels = g.OrderBy(i => i.InventoryMonth)
                                  .Select(i => i.InventoryMonth.HasValue ? i.InventoryMonth.Value.ToString("yyyy-MM") : "Unknown")
                                  .ToList(),
                        Quantities = g.OrderBy(i => i.InventoryMonth)
                                      .Select(i => i.Quantity)
                                      .ToList()
                    }
                );

            var highAlerts = _cvContext.VwNewsProductDisruptionDetails.Where(_ => _.SeverityRatingId == 4 || _.SeverityRatingId == 5).OrderByDescending(_ => _.PublishedDateUtc).ToList();
            var alertData = highAlerts.GroupBy(g => new { g.Title, g.Description, g.SeverityRatingId, g.PublishedDateUtc, g.Country, g.ArticleUrl })
                            .Select(group => new HighAlertViewModel
                            {
                                Title = group.Key.Title,
                                Description = group.Key.Description,
                                SeverityRatingId = group.Key.SeverityRatingId,
                                PublishedDateUtc = group.Key.PublishedDateUtc,
                                Country = group.Key.Country,
                                ArticleUrl = group.Key.ArticleUrl,
                                Materials = string.Join(", ", group.Select(_ => _.MaterialName).Distinct())
                            }).ToList();

            var utcNow = DateTime.UtcNow;

            var lowSeverityNews = _cvContext.VwNewsProductDisruptionDetails.Where(_ => _.SeverityRatingId < 4 && _.PublishedDateUtc > utcNow.AddDays(-1)).OrderByDescending(_ => _.PublishedDateUtc).ToList();
            var todayData = lowSeverityNews.GroupBy(g => new { g.Title, g.Description, g.SeverityRatingId, g.PublishedDateUtc, g.Country, g.ArticleUrl })
                            .Select(group => new HighAlertViewModel
                            {
                                Title = group.Key.Title,
                                Description = group.Key.Description,
                                SeverityRatingId = group.Key.SeverityRatingId,
                                PublishedDateUtc = group.Key.PublishedDateUtc,
                                Country = group.Key.Country,
                                ArticleUrl = group.Key.ArticleUrl,
                                Materials = string.Join(", ", group.Select(_ => _.MaterialName).Distinct())
                            }).ToList();

            foreach (var alert in alertData)
            {
                var hexcode = _cvContext.SeverityRatings.Where(_ => _.Id == alert.SeverityRatingId).Select(_ => _.Hexcode).FirstOrDefault();
                alert.SeverityRatingHexcode = hexcode;
            }

            foreach(var t in todayData)
            {
                var hexcode = _cvContext.SeverityRatings.Where(_ => _.Id == t.SeverityRatingId).Select(_ => _.Hexcode).FirstOrDefault();
                t.SeverityRatingHexcode = hexcode;
            }

            var latestTime = _cvContext.UpdatedTimes.Select(_ => _.LastUpdatedTimeUtc).OrderByDescending(_ => _).First();
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc((DateTime)latestTime, easternZone);
            // Prepare a model to pass to the view
            var data = new DashboardViewModel
            {
                CocoaData = cocoaChartData,
                SpringWheatData = springWheatChartData,
                SugarData = sugarChartData,
                WheatData = wheatChartData,
                Inventory = inventoryChartData,
                AlertNewsData = alertData,
                TodayNewsData = todayData,
                LatestUpdated = easternTime
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
