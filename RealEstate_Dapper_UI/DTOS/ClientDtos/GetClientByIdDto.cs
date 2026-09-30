namespace RealEstate_Dapper_UI.DTOS.ClientDtos
{
    public class GetClientByIdDto
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
