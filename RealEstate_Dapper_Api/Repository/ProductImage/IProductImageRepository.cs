using RealEstate_Dapper_Api.DTOS.ProductImageDtos;

namespace RealEstate_Dapper_Api.Repository.ProductImage
{
    public interface IProductImageRepository
    {

        Task<List<ResultProductImageDto>> GetAllAsync();
        Task<GetProductImageByIdDto> GetProductImageByIdAsync(int id);
        Task<List<GetProductImageByProductIdDto>> GetProductImageByProductAsync(int productId);
        Task UpdateAsync(UpdateProductImageDto dto);
        Task CreateAsync(CreateProductImageDto dto);  
        Task DeleteAsync(int id);
    }
}
