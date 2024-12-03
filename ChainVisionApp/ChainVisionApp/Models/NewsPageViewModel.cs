using ChainVision.Data.Models;

namespace ChainVisionApp.Models
{
    public class NewsPageViewModel
    {
        public List<NewsViewData> NewsData { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? SortBy { get; set; }
    }
}
