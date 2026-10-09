using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.AmenityDtos;

namespace RealEstate_Dapper_Api.Repository.Amenity
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly DapperContext _context;

        public AmenityRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateAmenityDto dto)
        {
            string query = "insert into Amenity (Title) values(@Title)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", dto.Title);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Amenity Where AmenityId=@AmenityId";
            var parameters = new DynamicParameters();
            parameters.Add("@AmenityId",id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultAmenityDto>> GetAllAsync()
        {
            string query = "Select * From Amenity";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAmenityDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetAmenityByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From Amenity Where AmenityId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetAmenityByIdDto>(query,parameters);
            }
        }

        public async Task UpdateAsync(UpdateAmenityDto dto)
        {
            string query = "Update Amenity Set Title=@Title Where AmenityId=@AmenityId";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", dto.Title);
            parameters.Add("@AmenityId", dto.AmenityId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
