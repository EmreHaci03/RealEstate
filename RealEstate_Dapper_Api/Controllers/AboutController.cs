
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.AboutDtos;
using RealEstate_Dapper_Api.Repository.About;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutRepository _aboutRepository;

        public AboutController(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values = await _aboutRepository.GetAllAsync();
            return Ok(values);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(int id)
        {
            var employee = await _aboutRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound("Hakkımızda Alanı Bilgileri Alınamadı");
            return Ok(employee);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto dto)
        {
            await _aboutRepository.CreateAsync(dto);
            return Ok("Hakkımızda Alanı Eklendi");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var employee = await _aboutRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound("Hakkımızda Alanı Bilgileri Alınamadı");
            await _aboutRepository.DeleteAsync(id);
            return Ok("Hakkımızda Silindi");
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto dto)
        {
            await _aboutRepository.UpdateAsync(dto);
            return Ok("Hakkımızda Başarıyla Güncellendi");
        }
    }
}
