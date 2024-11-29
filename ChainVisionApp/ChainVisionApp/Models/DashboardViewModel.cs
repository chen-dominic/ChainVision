using ChainVisionApp.Models.Data;

namespace ChainVisionApp.Models
{
    public class DashboardViewModel
    {
        public List<IngredientsDataCocoa> CocoaData { get; set; }
        public List<IngredientsDataSpringWheat> SpringWheatData { get; set; }
        public List<IngredientsDataSugar> SugarData { get; set; }
        public List<IngredientsDataWheat> WheatData { get; set; }
        public List<VwRecentHighSeverityNews> AlertNewsData { get; set; }
    }
}
