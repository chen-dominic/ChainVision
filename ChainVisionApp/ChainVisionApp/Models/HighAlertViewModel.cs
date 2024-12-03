namespace ChainVisionApp.Models
{
    public class HighAlertViewModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte? SeverityRatingId { get; set; }
        public string? SeverityRating { get; set; }
        public string? SeverityRatingHexcode { get; set; }
        public DateTime PublishedDateUtc { get; set; }
        public string? Country { get; set; }
        public string ArticleUrl { get; set; } = null!;
        public string Materials { get; set; } = null!;
    }
}
