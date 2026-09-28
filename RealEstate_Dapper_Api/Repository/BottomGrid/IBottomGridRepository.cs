using RealEstate_Dapper_Api.DTOS.BottomGridDtos;

namespace RealEstate_Dapper_Api.Repository.BottomGrid
{
    public interface IBottomGridRepository
    {
        Task<List<ResultBottomGridDto>> GetAllAsync();
        Task CreateAsync(CreateBottomGridDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateBottomGridDto dto);
        Task<GetBottomGridByIdDto> GetByIdAsync(int id);
    }
}
