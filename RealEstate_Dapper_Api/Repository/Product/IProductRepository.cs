using RealEstate_Dapper_Api.DTOS.ProductDtos;

namespace RealEstate_Dapper_Api.Repository.Product
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllAsync();
        Task<List<ResultProductWithCategoryDto>> ResultProductWithCategoryAsync();
        Task<List<ResultProductWithCategoryAndEmployeeDto>> ResultProductWithCategoryAndEmployeeAsync();
        Task<GetProductByIdDto> GetByIdAsync(int id);

        Task CreateAsync(CreateProductDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateProductDto dto);

    }
}
