using System;
using System.Collections.Generic;

namespace ChainVisionApp.Models.Data
{
    public partial class ProductDisruption
    {
        public int? NewsId { get; set; }
        public int? ProductId { get; set; }
        public int? DisruptionId { get; set; }
    }
}
