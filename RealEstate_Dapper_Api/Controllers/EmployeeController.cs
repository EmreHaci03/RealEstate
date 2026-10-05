using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.EmployeeDtos;
using RealEstate_Dapper_Api.Repository.Employee;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            var values = await employeeRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("ActiveEmployees")]
        public async Task<IActionResult> GetActiveEmployee()
        {
            var values = await employeeRepository.GetActiveEmployees();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound("Çalışan Bilgileri Alınamadı");
            return Ok(employee);
        }

        [HttpGet("GetEmployeeByProduct")]
        public async Task<IActionResult> GetEmployeeByProductId(int productId)
        {
            var values=await employeeRepository.GetEmployeeByProductIdAsync(productId);
            if (values == null)
                return NotFound("İlan Sahibi Bulunamadı");
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
        {
            await employeeRepository.CreateAsync(dto);
            return Ok("Çalışan Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound("Çalışan Bilgileri Alınamadı");
            await employeeRepository.DeleteAsync(id);
            return Ok("Çalışan Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto dto)
        {
            await employeeRepository.UpdateAsync(dto);
            return Ok("Çalışan Başarıyla Güncellendi");
        }


    }
}
