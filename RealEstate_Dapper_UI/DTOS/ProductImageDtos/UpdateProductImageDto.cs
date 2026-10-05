namespace RealEstate_Dapper_UI.DTOS.ProductImageDtos
{
    public class UpdateProductImageDto
    {
        public int ProductImageId { get; set; }
        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
        public int Product { get; set; }
    }
}
