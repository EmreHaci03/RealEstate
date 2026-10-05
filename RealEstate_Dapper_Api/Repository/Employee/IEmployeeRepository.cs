using RealEstate_Dapper_Api.DTOS.EmployeeDtos;

namespace RealEstate_Dapper_Api.Repository.Employee
{
    public interface IEmployeeRepository
    {
        Task<List<ResultEmployeeDto>> GetAllAsync();
        Task<List<ResultEmployeeDto>> GetActiveEmployees();
        Task<GetEmployeeByIdDto> GetByIdAsync(int id);

        Task<GetEmployeeByProductIdDto?> GetEmployeeByProductIdAsync(int ProductId);

        Task CreateAsync(CreateEmployeeDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateEmployeeDto dto);
    }
}
