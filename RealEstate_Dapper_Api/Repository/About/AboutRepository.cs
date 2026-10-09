using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.AboutDtos;

namespace RealEstate_Dapper_Api.Repository.About
{
    public class AboutRepository : IAboutRepository
    {
        private readonly DapperContext _context;

        public AboutRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateAboutDto dto)
        {
            string query = "insert into About(Title,Description,PhoneNumber) values(@Title,@Description,@PhoneNumber)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Description",dto.Description);
            parameters.Add("@Description",dto.Description);
            parameters.Add("@PhoneNumber", dto.PhoneNumber);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From About Where AboutId=@AboutId";
            var parameters = new DynamicParameters();
            parameters.Add("@AboutId",id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultAboutDto>> GetAllAsync()
        {
            string query = "Select * From About";
            using (var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultAboutDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetAboutByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From About Where AboutId=@AboutId";
            var parameters = new DynamicParameters();
            parameters.Add("@AboutId", id);

            using(var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetAboutByIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateAboutDto dto)
        {
            string query = "Update About Set Title=@Title, Description=@Description , PhoneNumber=@PhoneNumber Where AboutId=@AboutId";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@AboutId",dto.AboutId);
            parameters.Add("@PhoneNumber", dto.PhoneNumber);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
