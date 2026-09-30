namespace RealEstate_Dapper_Api.DTOS.ClientDtos
{
    public class UpdateClientDto
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
