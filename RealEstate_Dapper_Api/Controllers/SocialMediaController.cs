using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.SocialMediaDtos;
using RealEstate_Dapper_Api.Repository.SocialMedia;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaRepository _socialMediaRepository;

        public SocialMediaController(ISocialMediaRepository socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> SocialMediaList()
        {
            var values = await _socialMediaRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialMediaById(int id)
        {
            var value = await _socialMediaRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Sosyal Medya Hesabı Bulunamadı");

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto dto)
        {
            await _socialMediaRepository.CreateAsync(dto);
            return Ok("Sosyal Medya Hesabı Başarıyla Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto dto)
        {
            var value = await _socialMediaRepository.GetByIdAsync(dto.SocialMediaId);
            if (value == null)
                return NotFound("Sosyal Medya Hesabı Bulunamadı");

            await _socialMediaRepository.UpdateAsync(dto);
            return Ok("Sosyal Medya Hesabı Başarıyla Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var value = await _socialMediaRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Sosyal Medya Hesabı Bulunamadı");

            await _socialMediaRepository.DeleteAsync(id);
            return Ok("Sosyal Medya Hesabı Başarıyla Silindi");
        }
    }
}