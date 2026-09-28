using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.BottomGridDtos;

namespace RealEstate_Dapper_Api.Repository.BottomGrid
{
    public class BottomGridRepository : IBottomGridRepository
    {
        private readonly DapperContext _context;

        public BottomGridRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateBottomGridDto dto)
        {
            string query = "Insert Into BottomGrid (Icon, Title, Description) Values (@Icon, @Title, @Description)";
            var parameters = new DynamicParameters();
            parameters.Add("@Icon", dto.Icon);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Description", dto.Description);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From BottomGrid Where BottomGridId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultBottomGridDto>> GetAllAsync()
        {
            string query = "Select * From BottomGrid";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultBottomGridDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetBottomGridByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From BottomGrid Where BottomGridId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetBottomGridByIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateBottomGridDto dto)
        {
            string query = "Update BottomGrid Set Icon = @Icon, Title = @Title, Description = @Description Where BottomGridId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@Icon", dto.Icon);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@id", dto.BottomGridId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}