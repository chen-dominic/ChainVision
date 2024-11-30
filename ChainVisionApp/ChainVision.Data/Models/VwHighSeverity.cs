using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models
{
    public partial class VwHighSeverity
    {
        public int NewsId { get; set; }
        public string ArticleId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? DisplayText { get; set; }
        public string NewsDescription { get; set; } = null!;
        public DateTime PublishedDateUtc { get; set; }
        public string? Country { get; set; }
        public string ArticleUrl { get; set; } = null!;
        public int Id { get; set; }
        public string? SeverityDescription { get; set; }
        public DateTime? LastUpdatedTimeUtc { get; set; }
    }
}
