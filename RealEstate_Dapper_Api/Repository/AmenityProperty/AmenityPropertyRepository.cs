using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.AmenityDtos;
using RealEstate_Dapper_Api.DTOS.AmenityPropertyDtos;

namespace RealEstate_Dapper_Api.Repository.AmenityProperty
{
    public class AmenityPropertyRepository:IAmenityPropertyRepository
    {
        private readonly DapperContext _context;

        public AmenityPropertyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> AmenityPropertyChangeStatus(int id)
        {
            string query = "Update AmenityProperty Set Status=CASE WHEN STATUS=1 THEN 0 ELSE 1 END WHERE AmenityPropertyId=@AmenityPropertyId";
            var parameters = new DynamicParameters();
            parameters.Add("@AmenityPropertyId", id);
            using (var connection = _context.CreateConnection())
            {
                var affected = await connection.ExecuteAsync(query, parameters);
                return affected > 0;
            }

        }

        public async Task CreateAsync(CreateAmenityPropertyDto dto)
        {
            string query = "insert into AmenityProperty(Product,Amenity,Status) values(@Product,@Amenity,@Status)";
            var parameters = new DynamicParameters();
            parameters.Add("@Product", dto.Product);
            parameters.Add("@Amenity", dto.Amenity);
            parameters.Add("@Status", dto.Status);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteAsync(int id)
        {
            string query = "Delete From AmenityProperty Where AmenityPropertyId=@AmenityPropertyId";
            var parameters = new DynamicParameters();
            parameters.Add("@AmenityPropertyId",id);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);        
            }
        }

        public async Task<List<ResultAmenityPropertyDto>> GetAllAsync()
        {
            string query = @"Select p.AmenityPropertyId, t.Title As Product, a.Title As Amenity, p.Status
                 From AmenityProperty p
                 Inner Join Amenity a On p.Amenity = a.AmenityId
                 Inner Join Product t On p.Product = t.ProductId";

            using (var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultAmenityPropertyDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultAmenityPropertyDto>> GetAmenityPropertByAmenityAsync(int id)
        {
            string query = @"Select p.AmenityPropertyId, t.Title As Product, a.Title As Amenity, p.Status
                 From AmenityProperty p
                 Inner Join Amenity a On p.Amenity = a.AmenityId
                 Inner Join Product t On p.Product = t.ProductId Where p.Amenity=@AmenityId";
            var parameters = new DynamicParameters();
            parameters.Add("@AmenityId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAmenityPropertyDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultAmenityPropertyDto>> GetAmenityPropertByProductAsync(int id)
        {
            string query = @"Select p.AmenityPropertyId, t.Title As Product, a.Title As Amenity, p.Status
                 From AmenityProperty p
                 Inner Join Amenity a On p.Amenity = a.AmenityId
                 Inner Join Product t On p.Product = t.ProductId Where p.Product=@ProductId";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", id);
            using (var connection = _context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultAmenityPropertyDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<GetAmenityByIdPropertyDto> GetByIdAsync(int id)
        {
            string query = @"Select p.AmenityPropertyId, p.Product, p.Amenity, p.Status,
                            t.Title As ProductTitle, a.Title As AmenityTitle
                     From AmenityProperty p
                     Left Join Amenity a On p.Amenity = a.AmenityId
                     Left Join Product t On p.Product = t.ProductId
                     Where p.AmenityPropertyId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetAmenityByIdPropertyDto>(query, parameters);
            }
        }

        public async Task<List<ResultAmenityPropertyDto>> GetTrueAmenityPropertByProductAsync(int id)
        {
            string query = @"Select p.AmenityPropertyId ,p.Product,p.Amenity, p.Status,
                        t.Title As Product, a.Title As Amenity
                 From AmenityProperty p
                 inner join Amenity a on p.Amenity=a.AmenityId
                 inner join Product t on p.Product=t.ProductId
                 Where p.Product=@id AND p.Status=1";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            using(var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultAmenityPropertyDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task UpdateAsync(UpdateAmenityPropertyDto dto)
        {
            string query = "Update AmenityProperty Set Product=@Product, Amenity=@Amenity,Status=@Status Where AmenityPropertyId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@Product",dto.Product);
            parameters.Add("@Amenity", dto.Amenity);
            parameters.Add("@Status", dto.Status);
            parameters.Add("@id",dto.AmenityPropertyId);
            using ( var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);   
            }
        }
    }
}
