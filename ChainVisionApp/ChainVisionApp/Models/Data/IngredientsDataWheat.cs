using System;
using System.Collections.Generic;

namespace ChainVisionApp.Models.Data
{
    public partial class IngredientsDataWheat
    {
        public string? Wheat { get; set; }
        public string? Last { get; set; }
        public TimeSpan? Change { get; set; }
        public TimeSpan? Open { get; set; }
        public double? High { get; set; }
        public TimeSpan? Low { get; set; }
        public TimeSpan? Previous { get; set; }
        public short? Volume { get; set; }
        public DateTime? Time { get; set; }
    }
}
