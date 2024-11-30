using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models
{
    public partial class VwRecentHighSeverityNews
    {
        public int NewsId { get; set; }
        public string ArticleId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? DisplayText { get; set; }
        public string Description { get; set; } = null!;
        public byte? SeverityRatingId { get; set; }
        public DateTime PublishedDateUtc { get; set; }
        public string? Country { get; set; }
        public string ArticleUrl { get; set; } = null!;
        public int MaterialId { get; set; }
        public string MaterialName { get; set; } = null!;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
