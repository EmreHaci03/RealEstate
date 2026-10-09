namespace RealEstate_Dapper_Api.DTOS.AmenityPropertyDtos
{
    public class UpdateAmenityPropertyDto
    {
        public int AmenityPropertyId { get; set; }
        public int Product { get; set; }
        public int Amenity { get; set; }
        public bool Status { get; set; }
    }
}
