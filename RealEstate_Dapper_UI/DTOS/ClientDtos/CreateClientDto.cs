namespace RealEstate_Dapper_UI.DTOS.ClientDtos
{
    public class CreateClientDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }
    }
}
