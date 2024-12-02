using System;
using System.Collections.Generic;

namespace ChainVision.Data.Models.Data
{
    public partial class SeverityRating
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Hexcode { get; set; }
    }
}
