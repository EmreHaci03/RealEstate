
using RealEstate_Dapper_Api.DTOS.ProductDtos;

namespace RealEstate_Dapper_Api.Repository.Product
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllAsync();
        Task<List<ResultProductWithCategoryDto>> ResultProductWithCategoryAsync();
        Task<bool> CreateAdvertByUserAsync(CreateProductDto dto, int userId);
        Task<List<ResultProductWithCategoryDto>> GetProductAdvertsListByUserAsync(int userId);

        Task<ResultProductDetailDto?> GetAdvertDetailByUserAsync(int productId, int userId);

        Task<List<ResultProductWithSearchListDto>> ResultProductWithSearchList(string? searchKeyValue, int? categoryId, string? city);
        Task<List<ResultProductWithCategoryDto>> GetProductAdvertsListByEmployeeAsync(int id);
        Task<List<ResultProductWithCategoryAndEmployeeDto>> ResultProductWithCategoryAndEmployeeAsync();

        Task<List<ResultProductCountByCityDto>> ResultProductWithCityCount();
        Task<GetProductByIdDto> GetByIdAsync(int id);

        Task<bool> DeleteAdvertByUserAsync(int userId,int productId);
        Task<bool> UpdateAdvertByUserAsync(UpdateAdvertDto dto,int userId);

        Task CreateAsync(CreateProductDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateProductDto dto);

    }
}
