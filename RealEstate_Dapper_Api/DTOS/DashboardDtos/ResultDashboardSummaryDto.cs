namespace RealEstate_Dapper_Api.DTOS.DashboardDtos
{
    public class ResultDashboardSummaryDto
    {
        public int TotalProduct {  get; set; }

        public int MonthMessage { get; set; }
        public int TotalProductValue { get; set; }

        public int ActiveSubscribers { get; set; }
    }
}
