namespace RealEstate_Dapper_UI.DTOS.ContactDtos
{
    public class GetContactByIdDto
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Mail { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}
