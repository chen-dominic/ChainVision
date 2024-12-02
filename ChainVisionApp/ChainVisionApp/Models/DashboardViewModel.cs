using ChainVision.Data.Models;

namespace ChainVisionApp.Models
{
    public class DashboardViewModel
    {
        public List<IngredientsDataCocoa> CocoaData { get; set; }
        public List<IngredientsDataSpringWheat> SpringWheatData { get; set; }
        public List<IngredientsDataSugar> SugarData { get; set; }
        public List<IngredientsDataWheat> WheatData { get; set; }
        public List<VwRecentHighSeverityNews> AlertNewsData { get; set; }
        public DateTime? LatestUpdated { get; set; }
        public List<MaterialInventory> Inventory { get; set; }
    }
}
