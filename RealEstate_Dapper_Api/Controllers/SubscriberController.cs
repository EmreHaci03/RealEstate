using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.SubscriberDtos;
using RealEstate_Dapper_Api.Repository.Subscriber;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberController(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }


        [HttpGet]
        public async Task<IActionResult> SubscriberList()
        {
            var values = await _subscriberRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscriberById(int id)
        {
            var value = await _subscriberRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Mail Bulunamadı");
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberDto dto)
        {
            var exists = await _subscriberRepository.EmailExistsAsync(dto.Email);
            if (exists)
                return BadRequest("Bu e-posta adresi zaten kayıtlı.");

            await _subscriberRepository.CreateAsync(dto);
            return Ok("Aboneliğiniz başarıyla oluşturuldu.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var value = await _subscriberRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Silinmek İstenen Mail Bulunamadı");

            await _subscriberRepository.DeleteAsync(id);
            return Ok("Mail Aboneliği Başarıyla Silindi");
        }

        [HttpPut("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(int id,bool isActive)
        {
            var subscriber=await _subscriberRepository.GetByIdAsync(id);
            if (subscriber == null)
                return NotFound("Abone bulunamadı");

            await _subscriberRepository.ChangeSubscribeStatusAsync(id,isActive);
            return Ok(isActive ? "Abone Aktif Edildi" : "Abone Pasif Edildi");

        }

    }
}
