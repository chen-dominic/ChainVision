namespace ChainVisionApp.Models
{
    public class NewsViewData
    {
        public int NewsId { get; set; }
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DisplayText { get; set; }
        public int SeverityId { get; set; }
        public string SeverityRating { get; set; }
        public string SeverityRatingColor { get; set; }
        public DateTime PublishedDateUtc { get; set; }
        public string Country { get; set; }
        public string ArticleUrl { get; set; }
    }
}
