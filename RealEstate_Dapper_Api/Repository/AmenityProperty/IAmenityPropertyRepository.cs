using RealEstate_Dapper_Api.DTOS.AmenityDtos;
using RealEstate_Dapper_Api.DTOS.AmenityPropertyDtos;

namespace RealEstate_Dapper_Api.Repository.AmenityProperty
{
    public interface IAmenityPropertyRepository
    {
        Task<List<ResultAmenityPropertyDto>> GetAllAsync();
        Task CreateAsync(CreateAmenityPropertyDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateAmenityPropertyDto dto);

        Task<bool> AmenityPropertyChangeStatus(int id);

        Task<GetAmenityByIdPropertyDto> GetByIdAsync(int id);
        Task<List<ResultAmenityPropertyDto>> GetAmenityPropertByProductAsync(int id);
        Task<List<ResultAmenityPropertyDto>> GetTrueAmenityPropertByProductAsync(int id);
        Task<List<ResultAmenityPropertyDto>> GetAmenityPropertByAmenityAsync(int id);
    }
}
