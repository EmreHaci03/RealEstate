namespace RealEstate_Dapper_Api.DTOS.SubscriberDtos
{
    public class ResultSubscriberDto
    {
        public int SubscriberId { get; set; }
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
