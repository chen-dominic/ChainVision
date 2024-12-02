using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models
{
    public partial class IngredientsDataSugar
    {
        public string? Sugar { get; set; }
        public string? Last { get; set; }
        public TimeSpan? Change { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public TimeSpan? Low { get; set; }
        public TimeSpan? Previous { get; set; }
        public int? Volume { get; set; }
        public DateTime? Time { get; set; }
    }
}
