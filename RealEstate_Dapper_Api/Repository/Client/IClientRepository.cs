using RealEstate_Dapper_Api.DTOS.ClientDtos;

namespace RealEstate_Dapper_Api.Repository.Client
{
    public interface IClientRepository
    {
        Task<List<ResultClientDto>> GetAllAsync();
        Task CreateAsync(CreateClientDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateClientDto dto);
        Task<GetClientByIdDto> GetByIdAsync(int id);
    }
}
