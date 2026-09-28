using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.ServiceDtos;

namespace RealEstate_Dapper_Api.Repository.Service
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DapperContext _context;

        public ServiceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateServiceDto dto)
        {
            string query = "Insert Into Service (ServiceName, ServiceStatus) Values (@ServiceName, @ServiceStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@ServiceName", dto.ServiceName);
            parameters.Add("@ServiceStatus", dto.ServiceStatus);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Service Where ServiceId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultServiceDto>> GetAllAsync()
        {
            string query = "Select * From Service";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultServiceDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetServiceByIdDto?> GetByIdAsync(int id)
        {
            string query = "Select * From Service Where ServiceId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetServiceByIdDto>(query, parameters);
            }
        }

        public async Task<List<ResultServiceDto>> TrueServiceStatusList()
        {
            string query = "Select * From Service Where ServiceStatus=1";
            using (var connection = _context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultServiceDto>(query);
                return values.ToList();
            }
        }

        public async Task UpdateAsync(UpdateServiceDto dto)
        {
            string query = "Update Service Set ServiceName = @ServiceName, ServiceStatus = @ServiceStatus Where ServiceId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@ServiceName", dto.ServiceName);
            parameters.Add("@ServiceStatus", dto.ServiceStatus);
            parameters.Add("@id", dto.ServiceId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}