using RealEstate_Dapper_Api.DTOS.AdressDtos;

namespace RealEstate_Dapper_Api.Repository.Adress
{
    public interface IAdressRepository
    {
        Task<List<ResultAdressDto>> GetAllAsync();
        Task CreateAsync(CreateAdressDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateAdressDto dto);
        Task<GetAdressByIdDto> GetByIdAsync(int id);
    }
}
