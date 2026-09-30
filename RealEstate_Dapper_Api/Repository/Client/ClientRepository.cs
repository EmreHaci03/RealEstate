using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.ClientDtos;

namespace RealEstate_Dapper_Api.Repository.Client
{
    public class ClientRepository : IClientRepository
    {
        private readonly DapperContext _context;

        public ClientRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateClientDto dto)
        {
            string query = "insert into Client(Name,Title,Comment) values(@Name,@Title,@Comment)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name",dto.Name);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Comment", dto.Comment);
            
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Client Where ClientId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task<List<ResultClientDto>> GetAllAsync()
        {
            string query = "Select * From Client";

            using(var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultClientDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetClientByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From Client Where ClientId=@id";
            var parameters= new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetClientByIdDto>(query,parameters);
            }
        }

        public async Task UpdateAsync(UpdateClientDto dto)
        {
            string query = "Update Client Set Name=@Name,Title=@Title,Comment=@Comment Where ClientId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", dto.Name);
            parameters.Add("@id", dto.ClientId);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Comment", dto.Comment);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
