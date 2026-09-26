using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.ProductDetailDtos;
using RealEstate_Dapper_Api.Repository.ProductDetailDto;

namespace RealEstate_Dapper_Api.Repository.ProductDetail
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly DapperContext _context;

        public ProductDetailRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateProductDetailDto dto)
        {
            string query = "insert into ProductDetails(ProductSize,BedRoomCount,BathCount,RoomCount,GarageSize,BuildYear,Price,Location,VideoUrl,Product)" +
                " values(@ProductSize,@BedRoomCount,@BathCount,@RoomCount,@GarageSize,@BuildYear,@Price,@Location,@VideoUrl,@Product)";

            var parameters = new DynamicParameters();
            parameters.Add("@ProductSize", dto.ProductSize);
            parameters.Add("@BedRoomCount", dto.BedRoomCount);
            parameters.Add("@BathCount", dto.BathCount);
            parameters.Add("@RoomCount", dto.RoomCount);
            parameters.Add("@GarageSize", dto.GarageSize);
            parameters.Add("@BuildYear", dto.BuildYear);
            parameters.Add("@Price", dto.Price);
            parameters.Add("@Location", dto.Location);
            parameters.Add("@VideoUrl", dto.VideoUrl);
            parameters.Add("@Product", dto.Product);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From ProductDetails Where ProductDetailId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultProductDetailDto>> GetAllAsync()
        {
            string query = "select d.ProductDetailId, d.ProductSize, d.BedRoomCount, d.BathCount, d.RoomCount, d.GarageSize, " +
                "d.BuildYear, d.Price, d.Location, d.VideoUrl, p.Title as Product " +
                "from ProductDetails d " +
                "inner join Product p on d.Product = p.ProductId";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDetailDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetProductDetailByIdDto?> GetByIdAsync(int id)
        {
            string query = "select d.ProductDetailId, d.ProductSize, d.BedRoomCount, d.BathCount, d.RoomCount, d.GarageSize, " +
                "d.BuildYear, d.Price, d.Location, d.VideoUrl, d.Product, p.Title as Product " +
                "from ProductDetails d " +
                "inner join Product p on d.Product = p.ProductId " +
                "where d.ProductDetailId = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetProductDetailByIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateProductDetailDto dto)
        {
            string query = "Update ProductDetails Set ProductSize=@ProductSize, BedRoomCount=@BedRoomCount, BathCount=@BathCount, " +
                "RoomCount=@RoomCount, GarageSize=@GarageSize, BuildYear=@BuildYear, Price=@Price, Location=@Location, " +
                "VideoUrl=@VideoUrl, Product=@Product Where ProductDetailId=@ProductDetailId";

            var parameters = new DynamicParameters();
            parameters.Add("@ProductSize", dto.ProductSize);
            parameters.Add("@BedRoomCount", dto.BedRoomCount);
            parameters.Add("@BathCount", dto.BathCount);
            parameters.Add("@RoomCount", dto.RoomCount);
            parameters.Add("@GarageSize", dto.GarageSize);
            parameters.Add("@BuildYear", dto.BuildYear);
            parameters.Add("@Price", dto.Price);
            parameters.Add("@Location", dto.Location);
            parameters.Add("@VideoUrl", dto.VideoUrl);
            parameters.Add("@Product", dto.Product);
            parameters.Add("@ProductDetailId", dto.ProductDetailId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}