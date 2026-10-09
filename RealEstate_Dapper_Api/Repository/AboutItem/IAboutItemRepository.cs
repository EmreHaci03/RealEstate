using RealEstate_Dapper_Api.DTOS.AboutItemDtos;

namespace RealEstate_Dapper_Api.Repository.AboutItem
{
    public interface IAboutItemRepository
    {
        Task<List<ResultAboutItemDto>> GetAllAsync();
        Task<GetAboutItemByIdDto> GetAboutItemByIdDto(int id);
        Task<List<GetAboutItemByIdDto>> GetAboutItemsByAboutAsync(int aboutId);
        Task UpdateAsync(UpdateAboutItemDto dto);
        Task DeleteAsync(int id);
        Task CreateAsync(CreateAboutItemDto dto);
    }
}
