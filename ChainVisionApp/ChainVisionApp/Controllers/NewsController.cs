using ChainVisionApp.Controllers;
using ChainVisionApp.Models;
using ChainVisionApp.Hubs;
using ChainVision.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading;

public class NewsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ChainVisionContext _cvContext;
    private readonly IHubContext<NewsHub> _hubContext;

    public NewsController(ILogger<HomeController> logger, ChainVisionContext cvContext, IHubContext<NewsHub> hubContext)
    {
        _logger = logger;
        _cvContext = cvContext;
        _hubContext = hubContext;
    }

    public IActionResult Index(string sortBy = "date")
    {
        List<NewsViewData> newsData = new List<NewsViewData>();
        var news = _cvContext.News.OrderByDescending(_ => _.PublishedDateUtc).ToList();

        foreach ( var item in news)
        {
            var severity = _cvContext.SeverityRatings.Where(_ => _.Id == item.SeverityRatingId).Select(_ => _.Description).FirstOrDefault();
            var hexcode = _cvContext.SeverityRatings.Where(_ => _.Id == item.SeverityRatingId).Select(_ => _.Hexcode).FirstOrDefault();
            var newsDataItem = new NewsViewData
            {
                NewsId = item.Id,
                ArticleId = item.ArticleId,
                Title = item.Title,
                Description = item.Description,
                DisplayText = item.DisplayText,
                SeverityId = (int)item.SeverityRatingId,
                SeverityRating = severity ?? "N/A",
                SeverityRatingColor = hexcode ?? "#8c8c8c",
                PublishedDateUtc = item.PublishedDateUtc,
                Country = item.Country,
                ArticleUrl = item.ArticleUrl,
            };
            newsData.Add(newsDataItem);
        }
        if (sortBy == "date")
        {
            newsData = newsData.OrderByDescending(n => n.PublishedDateUtc).ToList();
        }
        else if (sortBy == "severity")
        {
            newsData = newsData.OrderByDescending(n => n.SeverityId).ToList();
        }

        var lastUpdated = _cvContext.UpdatedTimes.Select(_ => _.LastUpdatedTimeUtc).OrderByDescending(_ => _).FirstOrDefault();

        TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc((DateTime)lastUpdated, easternZone);

        var data = new NewsPageViewModel
        {
            NewsData = newsData,
            LastUpdated = easternTime,
            SortBy = sortBy
        };
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> NotifyUpdate()
    {
        var newsData = _cvContext.News.OrderByDescending(_ => _.PublishedDateUtc).ToList();
        var lastUpdated = _cvContext.UpdatedTimes.Select(_ => _.LastUpdatedTimeUtc).OrderByDescending(_ => _).FirstOrDefault();

        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", newsData, lastUpdated);
        return Ok();
    }
}
