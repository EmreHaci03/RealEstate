namespace RealEstate_Dapper_Api.DTOS.SocialMediaDtos
{
    public class GetSocialMediaByIdDto
    {
        public int SocialMediaId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string SocialMediaUrl { get; set; }
    }
}
