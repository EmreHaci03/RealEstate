using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.ProductImageDtos;

namespace RealEstate_Dapper_Api.Repository.ProductImage
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly DapperContext _context;

        public ProductImageRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateProductImageDto dto)
        {
            string query = "insert into ProductImage(ImageUrl,SortOrder,Product) values(@ImageUrl,@SortOrder,@Product)";
            var parameters = new DynamicParameters();
            parameters.Add("@ImageUrl", dto.ImageUrl);
            parameters.Add("@SortOrder", dto.SortOrder);
            parameters.Add("@Product", dto.Product);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From ProductImage Where ProductImageId=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultProductImageDto>> GetAllAsync()
        {
            string query = "Select ProductImageId,ImageUrl,SortOrder,p.Title as Product From ProductImage m  inner join Product p on m.Product=p.ProductId";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductImageDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetProductImageByIdDto> GetProductImageByIdAsync(int id)
        {
            string query = @"SELECT m.ProductImageId, m.ImageUrl, m.SortOrder, m.Product, p.Title
                             FROM ProductImage m
                             INNER JOIN Product p ON m.Product = p.ProductId
                             WHERE m.ProductImageId = @ProductImageId";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductImageId", id);  
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetProductImageByIdDto>(query, parameters);
            }
        }

        public async Task<List<GetProductImageByProductIdDto>> GetProductImageByProductAsync(int productId)
        {
            string query = @"SELECT m.ProductImageId, m.ImageUrl, m.SortOrder, m.Product, p.Title
                             FROM ProductImage m
                             INNER JOIN Product p ON m.Product = p.ProductId
                             WHERE m.Product = @ProductId
                             ORDER BY m.SortOrder";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", productId);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductImageByProductIdDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task UpdateAsync(UpdateProductImageDto dto)
        {
            string query = "Update ProductImage Set ImageUrl=@ImageUrl, SortOrder=@SortOrder, Product=@Product Where ProductImageId=@ProductImageId";

            var parameters = new DynamicParameters();
            parameters.Add("@ImageUrl", dto.ImageUrl);
            parameters.Add("@SortOrder", dto.SortOrder);
            parameters.Add("@Product", dto.Product); 
            parameters.Add("@ProductImageId", dto.ProductImageId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}