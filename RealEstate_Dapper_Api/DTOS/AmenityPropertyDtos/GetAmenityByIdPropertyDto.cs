namespace RealEstate_Dapper_Api.DTOS.AmenityPropertyDtos
{
    public class GetAmenityByIdPropertyDto
    {
        public int AmenityPropertyId { get; set; }
        public string Product { get; set; }
        public string Amenity { get; set; }
        public bool Status { get; set; }
    }
}
