using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models
{
    public partial class IngredientsDataSugar
    {
        public string Sugar { get; set; } = null!;
        public string Last { get; set; } = null!;
        public decimal Change { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public decimal Low { get; set; }
        public decimal? Previous { get; set; }
        public int Volume { get; set; }
        public DateTime? Time { get; set; }
    }
}
