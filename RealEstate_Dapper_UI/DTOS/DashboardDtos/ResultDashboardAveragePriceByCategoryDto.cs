namespace RealEstate_Dapper_UI.DTOS.DashboardDtos
{
    public class ResultDashboardAveragePriceByCategoryDto
    {
        public int AveragePrice { get; set; }
        public int TotalProductCount { get; set; }
        public string CategoryName { get; set; }
    }
}
