using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models
{
    public partial class MaterialInventory
    {
        public string Ingredient { get; set; } = null!;
        public DateTime InventoryMonth { get; set; }
        public double Quantity { get; set; }
    }
}
