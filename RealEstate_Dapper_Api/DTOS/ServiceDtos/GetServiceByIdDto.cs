namespace RealEstate_Dapper_Api.DTOS.ServiceDtos
{
    public class GetServiceByIdDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool ServiceStatus { get; set; }
    }
}
