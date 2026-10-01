namespace RealEstate_Dapper_UI.DTOS.ProductDtos
{
    public class ResultProductWithDetailDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string CoverImage { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string EmployeeName { get; set; }
    }
}
