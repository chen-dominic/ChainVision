using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models
{
    public partial class IngredientsDataSpringWheat
    {
        public string SpringWheat { get; set; } = null!;
        public string Last { get; set; } = null!;
        public TimeSpan Change { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public TimeSpan Low { get; set; }
        public decimal? Previous { get; set; }
        public short Volume { get; set; }
        public DateTime? Time { get; set; }
    }
}
