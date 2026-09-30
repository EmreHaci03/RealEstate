namespace RealEstate_Dapper_UI.DTOS.AdressDtos
{
    public class GetAdressByIdDto
    {
        public int AdressId { get; set; }
        public string AdressTitle1 { get; set; }
        public string Description { get; set; }
        public string AdressTitle2 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
