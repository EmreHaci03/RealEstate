using RealEstate_Dapper_Api.DTOS.ProductDtos;

namespace RealEstate_Dapper_Api.Repository.Product
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllAsync();
        Task<List<ResultProductWithCategoryDto>> ResultProductWithCategoryAsync();
        Task<List<ResultProductWithCategoryDto>> GetProductAdvertsListByUserAsync(int userId);

        Task<List<ResultProductWithCategoryDto>> GetProductAdvertsListByEmployeeAsync(int id);
        Task<List<ResultProductWithCategoryAndEmployeeDto>> ResultProductWithCategoryAndEmployeeAsync();

        Task<List<ResultProductCountByCityDto>> ResultProductWithCityCount();
        Task<GetProductByIdDto> GetByIdAsync(int id);

        Task CreateAsync(CreateProductDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateProductDto dto);

    }
}
