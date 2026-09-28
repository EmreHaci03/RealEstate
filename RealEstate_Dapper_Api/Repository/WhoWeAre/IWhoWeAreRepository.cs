using RealEstate_Dapper_Api.DTOS.ProductDetailDtos;
using RealEstate_Dapper_Api.DTOS.WhoWeAreDtos;

namespace RealEstate_Dapper_Api.Repository.WhoWeAre
{
    public interface IWhoWeAreRepository
    {
        Task<List<ResultWhoWeAreDetailDto>> GetAllAsync();
        Task<GetWhoWeAreDetailByIdDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateWhoWeAreDetailDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateWhoWeAreDetailDto dto);
    }
}
