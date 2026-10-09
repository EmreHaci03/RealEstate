namespace RealEstate_Dapper_UI.DTOS.AmenityPropertyDtos
{
    public class GetAmenityPropertyByIdDto
    {
        public int AmenityPropertyId { get; set; }
        public string Product { get; set; }
        public string Amenity { get; set; }
        public bool Status { get; set; }
    }
}
