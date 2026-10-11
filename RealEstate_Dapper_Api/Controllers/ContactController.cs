using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.ContactDtos;
using RealEstate_Dapper_Api.Repository.Contact;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContact()
        {
            var values = await _contactRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var value = await _contactRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Mesaj bulunamadı");

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto dto)
        {
            await _contactRepository.CreateAsync(dto);
            return Ok("Mesajınız başarıyla gönderildi");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var value = await _contactRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Mesaj bulunamadı");

            await _contactRepository.DeleteAsync(id);
            return Ok("Mesaj başarıyla silindi");
        }
    }
}