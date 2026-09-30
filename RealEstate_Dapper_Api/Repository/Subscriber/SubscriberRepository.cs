using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.SubscriberDtos;

namespace RealEstate_Dapper_Api.Repository.Subscriber
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly DapperContext _context;

        public SubscriberRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task ChangeSubscribeStatusAsync(int id, bool isActive)
        {
            string query = "Update Subscriber Set IsActive = @isActive Where SubscriberId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@isActive", isActive);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task CreateAsync(CreateSubscriberDto dto)
        {
            string query = "Insert Into Subscriber (Email) Values (@Email)";
            var parameters = new DynamicParameters();
            parameters.Add("@Email", dto.Email);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete from Subscriber Where SubscriberId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            string query = "Select Count(1) From Subscriber Where Email = @Email";
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(query, parameters);
            }
        }

        public async Task<List<ResultSubscriberDto>> GetAllAsync()
        {
            string query = "Select * From Subscriber";

            using (var connection = _context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultSubscriberDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetSubscriberByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From Subscriber Where SubscriberId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);   

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetSubscriberByIdDto>(query,parameters);
            }
        }
    }
}
