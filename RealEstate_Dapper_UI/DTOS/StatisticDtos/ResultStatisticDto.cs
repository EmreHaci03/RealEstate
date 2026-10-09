namespace RealEstate_Dapper_UI.DTOS.StatisticDtos
{
    public class ResultStatisticDto
    {
        public int TotalProduct { get; set; }
        public int TotalCategory { get; set; }
        public int TotalEmployee { get; set; }
        public int ActiveEmployee { get; set; }
        public int ClientCount { get; set; }
        public int CityCount { get; set; }
        public int TotalCity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public string TopCity { get; set; }
        public string TopCategory { get; set; }
        public string TopEmployee { get; set; }
        public int ActiveSubscriber { get; set; }
    }

}
