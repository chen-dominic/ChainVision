using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models.Data
{
    public partial class IngredientsDataSpringWheat
    {
        public string? SpringWheat { get; set; }
        public string? Last { get; set; }
        public TimeSpan? Change { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public TimeSpan? Low { get; set; }
        public double? Previous { get; set; }
        public short? Volume { get; set; }
        public DateTime? Time { get; set; }
    }
}
