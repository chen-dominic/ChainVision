using ChainVision.Data.Models;

namespace ChainVisionApp.Models
{
    public class MaterialNewsModel
    {
        public string Material { get; set; }
        public List<HighAlertViewModel> NewsList { get; set; }
        public int NewsCount { get; set; }
    }
}
