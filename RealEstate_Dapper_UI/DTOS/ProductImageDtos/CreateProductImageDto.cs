namespace RealEstate_Dapper_UI.DTOS.ProductImageDtos
{
    public class CreateProductImageDto
    {
        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
        public int Product { get; set; }
    }
}
