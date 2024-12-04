using ChainVision.Data.Models;

namespace ChainVisionApp.Models
{
    public class DashboardViewModel
    {
        public List<MaterialDataViewModel> CocoaData { get; set; }
        public List<MaterialDataViewModel> SpringWheatData { get; set; }
        public List<MaterialDataViewModel> SugarData { get; set; }
        public List<MaterialDataViewModel> WheatData { get; set; }
        public List<MaterialNewsModel> AlertNewsData { get; set; }
        public List<HighAlertViewModel> TodayNewsData { get; set; }
        public DateTime? LatestUpdated { get; set; }
        public Dictionary<string, InventoryChartData> Inventory { get; set; }
    }
}
