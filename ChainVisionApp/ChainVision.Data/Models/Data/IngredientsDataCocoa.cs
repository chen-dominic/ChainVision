using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models.Data
{
    public partial class IngredientsDataCocoa
    {
        public string? Cocoa { get; set; }
        public string? Last { get; set; }
        public short? Change { get; set; }
        public short? Open { get; set; }
        public short? High { get; set; }
        public short? Low { get; set; }
        public short? Previous { get; set; }
        public short? Volume { get; set; }
        public DateTime? Time { get; set; }
    }
}
