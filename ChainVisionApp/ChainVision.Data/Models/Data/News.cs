using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models.Data
{
    public partial class News
    {
        public int Id { get; set; }
        public string ArticleId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? DisplayText { get; set; }
        public string Description { get; set; } = null!;
        public byte? SeverityRatingId { get; set; }
        public DateTime PublishedDateUtc { get; set; }
        public string? Country { get; set; }
        public string ArticleUrl { get; set; } = null!;
    }
}
