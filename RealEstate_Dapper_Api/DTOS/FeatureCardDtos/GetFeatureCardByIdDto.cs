namespace RealEstate_Dapper_Api.DTOS.FeatureCardDtos
{
    public class GetFeatureCardByIdDto
    {
        public int FeatureCardId { get; set; }
        public string Icon { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
    }
}
