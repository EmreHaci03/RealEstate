using RealEstate_Dapper_Api.DTOS.CategoryDtos;

namespace RealEstate_Dapper_Api.Repository.Category
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDto>> GetAllAsync();
        Task CreateAsync(CreateCategoryDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCategoryDto dto);
        Task<GetCategoryByIdDto> GetCategoryByIdAsync(int id);
    }
}
