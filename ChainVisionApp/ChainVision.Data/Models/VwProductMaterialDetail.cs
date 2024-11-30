using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models
{
    public partial class VwProductMaterialDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int? MaterialId { get; set; }
        public string? MaterialName { get; set; }
    }
}
