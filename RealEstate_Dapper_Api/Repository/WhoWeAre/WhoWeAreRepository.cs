using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.WhoWeAreDtos;

namespace RealEstate_Dapper_Api.Repository.WhoWeAre
{
    public class WhoWeAreRepository : IWhoWeAreRepository
    {
        private readonly DapperContext _context;

        public WhoWeAreRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateWhoWeAreDetailDto dto)
        {
            string query = "insert into WhoWeAreDetail(Title,SubTitle,Description1,Description2) values(@Title,@SubTitle,@Description1,@Description2)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title",dto.Title);
            parameters.Add("@SubTitle", dto.SubTitle);
            parameters.Add("@Description1", dto.Description1);
            parameters.Add("@Description2", dto.Description2);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From WhoWeAreDetail Where WhoWeAreDetailId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultWhoWeAreDetailDto>> GetAllAsync()
        {
            string query = "Select * From WhoWeAreDetail";

            using (var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultWhoWeAreDetailDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetWhoWeAreDetailByIdDto?> GetByIdAsync(int id)
        {
            string query = "Select * From WhoWeAreDetail Where WhoWeAreDetailId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetWhoWeAreDetailByIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateWhoWeAreDetailDto dto)
        {
            string query = "Update WhoWeAreDetail Set Title=@Title , SubTitle=@SubTitle, Description1=@Description1 , Description2=@Description2 Where WhoWeAreDetailId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", dto.Title);
            parameters.Add("@SubTitle", dto.SubTitle);
            parameters.Add("@Description1", dto.Description1);
            parameters.Add("@Description2", dto.Description2);
            parameters.Add("@id", dto.WhoWeAreDetailId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
