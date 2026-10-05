using RealEstate_Dapper_Api.DTOS.EmployeeDtos;
using RealEstate_Dapper_Api.DTOS.ProductDetailDtos;

namespace RealEstate_Dapper_Api.Repository.ProductDetailDto
{
    public interface IProductDetailRepository
    {
        Task<List<ResultProductDetailDto>> GetAllAsync();

        Task<GetProductDetailByIdDto> GetProductDetailByProductId(int ProductId);
        Task<GetProductDetailByIdDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDetailDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateProductDetailDto dto);
    }
}
