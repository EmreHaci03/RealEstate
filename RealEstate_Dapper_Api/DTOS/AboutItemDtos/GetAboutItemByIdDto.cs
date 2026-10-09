namespace RealEstate_Dapper_Api.DTOS.AboutItemDtos
{
    public class GetAboutItemByIdDto
    {
        public int AboutItemId { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string About { get; set; }

    }
}
