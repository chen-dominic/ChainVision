using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models.Data
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
