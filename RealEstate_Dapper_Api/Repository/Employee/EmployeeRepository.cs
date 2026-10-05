using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.EmployeeDtos;

namespace RealEstate_Dapper_Api.Repository.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateEmployeeDto dto)
        {
            string query = "insert into Employee(Name,Title,Mail,PhoneNumber,ImageUrl,Status) " +
                "values(@Name,@Title,@Mail,@PhoneNumber,@ImageUrl,@Status)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", dto.Name);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Mail", dto.Mail);
            parameters.Add("@PhoneNumber", dto.PhoneNumber);
            parameters.Add("@ImageUrl", dto.ImageUrl);
            parameters.Add("@Status", true);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Employee Where EmployeeId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultEmployeeDto>> GetActiveEmployees()
        {
            string query = "Select * From Employee Where Status=1";

            using(var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetAllAsync()
        {
            string query = "Select EmployeeId,Name,Title,Mail,PhoneNumber,ImageUrl,Status From Employee";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetEmployeeByIdDto?> GetByIdAsync(int id)
        {
            string query = "Select EmployeeId,Name,Title,Mail,PhoneNumber,ImageUrl,Status From Employee Where EmployeeId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetEmployeeByIdDto>(query, parameters);
            }
        }

        public async Task<GetEmployeeByProductIdDto?> GetEmployeeByProductIdAsync(int ProductId)
        {
            string query = @"SELECT e.EmployeeId, e.Name, e.Title, e.Mail, e.PhoneNumber, e.ImageUrl
                     FROM Product p
                     INNER JOIN Employee e ON p.Employee = e.EmployeeId
                     WHERE p.ProductId = @ProductId";

            var parameters= new DynamicParameters();
            parameters.Add("@ProductId",ProductId);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetEmployeeByProductIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateEmployeeDto dto)
        {
            string query = "Update Employee Set Name=@Name, Title=@Title, Mail=@Mail, PhoneNumber=@PhoneNumber, " +
                "ImageUrl=@ImageUrl, Status=@Status Where EmployeeId=@EmployeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", dto.Name);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Mail", dto.Mail);
            parameters.Add("@PhoneNumber", dto.PhoneNumber);
            parameters.Add("@ImageUrl", dto.ImageUrl);
            parameters.Add("@Status", dto.Status);
            parameters.Add("@EmployeeId", dto.EmployeeId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}