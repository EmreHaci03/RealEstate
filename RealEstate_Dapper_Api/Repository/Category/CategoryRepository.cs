using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.CategoryDtos;

namespace RealEstate_Dapper_Api.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _context;

        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            string query = "insert into Category(CategoryName,CategoryStatus) values(@CategoryName,@CategoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryName", dto.CategoryName);
            parameters.Add("@CategoryStatus", true);
            using(var connection= _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Category Where CategoryId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
               
            }
        }

        public async Task<List<ResultCategoryDto?>> GetAllAsync()
        {
            string query = "Select * From Category";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();

            }
        }

        public async Task<GetCategoryByIdDto?> GetCategoryByIdAsync(int id)
        {
            string query = "Select CategoryId, CategoryName, CategoryStatus From Category Where CategoryId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetCategoryByIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            string query = "Update Category Set CategoryName=@CategoryName, CategoryStatus=@CategoryStatus Where CategoryId=@CategoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryName", dto.CategoryName);
            parameters.Add("@CategoryStatus", dto.CategoryStatus);
            parameters.Add("@CategoryId", dto.CategoryId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
