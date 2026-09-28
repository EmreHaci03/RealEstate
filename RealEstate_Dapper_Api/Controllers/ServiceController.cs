using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.ServiceDtos;
using RealEstate_Dapper_Api.Repository.Employee;
using RealEstate_Dapper_Api.Repository.Service;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ServiceList()
        {
            var values = await serviceRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("GetActiveServices")]
        public async Task<IActionResult> GetActiveServiceList()
        {
            var values = await serviceRepository.TrueServiceStatusList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var Service = await serviceRepository.GetByIdAsync(id);
            if (Service == null)
                return NotFound("Hizmet Bilgileri Alınamadı");
            return Ok(Service);
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto dto)
        {
            await serviceRepository.CreateAsync(dto);
            return Ok("Hizmet Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var Service = await serviceRepository.GetByIdAsync(id);
            if (Service == null)
                return NotFound("Hizmet Bilgileri Alınamadı");
            await serviceRepository.DeleteAsync(id);
            return Ok("Hizmet Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
        {
            await serviceRepository.UpdateAsync(dto);
            return Ok("Hizmet Başarıyla Güncellendi");
        }
    }
}
