using RealEstate_Dapper_Api.DTOS.ServiceDtos;

namespace RealEstate_Dapper_Api.Repository.Service
{
    public interface IServiceRepository
    {
        Task<List<ResultServiceDto>> GetAllAsync();

        Task<List<ResultServiceDto>> TrueServiceStatusList();
        Task CreateAsync(CreateServiceDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateServiceDto dto);
        Task<GetServiceByIdDto?> GetByIdAsync(int id);
    }
}
