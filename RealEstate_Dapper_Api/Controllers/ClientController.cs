using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.ClientDtos;
using RealEstate_Dapper_Api.Repository.Client;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ClientList()
        {
            var values = await _clientRepository.GetAllAsync();
            return Ok(values);  
        }
        [HttpGet("GetActiveClients")]
        public async Task<IActionResult> GetActiveClient()
        {
            var values=await _clientRepository.GetActiveClientsAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var value = await _clientRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Müşteri Bulunamadı");

           return Ok(value);
        }
        [HttpPost]
        public  async Task<IActionResult> CreateClient(CreateClientDto dto)
        {
            await _clientRepository.CreateAsync(dto);
            return Ok("Müşteri Başarıyla Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client=await _clientRepository.GetByIdAsync(id);
            if (client == null)
                return NotFound("Müşteri Bilgileri Alınamadı");

            await _clientRepository.DeleteAsync(id);
            return Ok("Müşteri Başarıyla Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(UpdateClientDto dto)
        {
            var client = await _clientRepository.GetByIdAsync(dto.ClientId);
            if (client == null)
                return NotFound("Müşteri Bulunamadı");

            await _clientRepository.UpdateAsync(dto);
            return Ok("Müşteri Başarıyla Güncellendi");
        }
    }
}
