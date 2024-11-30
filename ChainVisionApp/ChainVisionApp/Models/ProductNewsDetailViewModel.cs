using ChainVisionApp.Models.Data;

namespace ChainVisionApp.Models
{
    public class ProductNewsDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string? Ingredients { get; set; }
        public List<NewsMaterialData>? NewsMaterialData { get; set; }
    }
}
