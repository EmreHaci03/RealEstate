using RealEstate_Dapper_Api.DTOS.AmenityDtos;

namespace RealEstate_Dapper_Api.Repository.Amenity
{
    public interface IAmenityRepository
    {
        Task<List<ResultAmenityDto>> GetAllAsync();
        Task CreateAsync(CreateAmenityDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateAmenityDto dto);
        Task<GetAmenityByIdDto> GetByIdAsync(int id);
    }
}
