namespace RealEstate_Dapper_Api.DTOS.ContactDtos
{
    public class CreateContactDto
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Mail { get; set; }
        public string Message { get; set; }
    }
}
