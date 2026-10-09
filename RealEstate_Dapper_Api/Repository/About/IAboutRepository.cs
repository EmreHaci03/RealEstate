using RealEstate_Dapper_Api.DTOS.AboutDtos;

namespace RealEstate_Dapper_Api.Repository.About
{
    public interface IAboutRepository
    {
        Task<List<ResultAboutDto>> GetAllAsync();
        Task CreateAsync(CreateAboutDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateAboutDto dto);
        Task<GetAboutByIdDto> GetByIdAsync(int id);
    }
}
