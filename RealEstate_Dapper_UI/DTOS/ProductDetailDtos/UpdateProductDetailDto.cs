namespace RealEstate_Dapper_UI.DTOS.ProductDetailDtos
{
    public class UpdateProductDetailDto
    {
        public int ProductDetailId { get; set; }
        public string ProductSize { get; set; }
        public int BedRoomCount { get; set; }
        public int BathCount { get; set; }
        public int RoomCount { get; set; }
        public int GarageSize { get; set; }
        public string BuildYear { get; set; }
        public decimal Price { get; set; }
        public string? Location { get; set; }
        public string? VideoUrl { get; set; }
        public int Product { get; set; }
    }
}
