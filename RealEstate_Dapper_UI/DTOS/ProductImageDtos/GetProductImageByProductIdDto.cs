namespace RealEstate_Dapper_UI.DTOS.ProductImageDtos
{
    public class GetProductImageByProductIdDto
    {
        public int ProductImageId { get; set; }
        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
        public string Product { get; set; }
    }
}
