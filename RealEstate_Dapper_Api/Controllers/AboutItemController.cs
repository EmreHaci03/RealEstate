using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.AboutItemDtos;
using RealEstate_Dapper_Api.Repository.AboutItem;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutItemController : ControllerBase
    {
        private readonly IAboutItemRepository _aboutItemRepository;

        public AboutItemController(IAboutItemRepository aboutItemRepository)
        {
            _aboutItemRepository = aboutItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AboutItemList()
        {
            var values = await _aboutItemRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutItem(int id)
        {
            var value = await _aboutItemRepository.GetAboutItemByIdDto(id);
            if (value == null)
                return NotFound("Hakkımızda Maddesi Bulunamadı");
            return Ok(value);
        }

        [HttpGet("GetAboutItemsByAbout/{aboutId}")]
        public async Task<IActionResult> GetAboutItemsByAbout(int aboutId)
        {
            var values = await _aboutItemRepository.GetAboutItemsByAboutAsync(aboutId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutItem(CreateAboutItemDto dto)
        {
            await _aboutItemRepository.CreateAsync(dto);
            return Ok("Hakkımızda Maddesi Başarıyla Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAboutItem(UpdateAboutItemDto dto)
        {
            await _aboutItemRepository.UpdateAsync(dto);
            return Ok("Hakkımızda Maddesi Başarıyla Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutItem(int id)
        {
            var value = await _aboutItemRepository.GetAboutItemByIdDto(id);
            if (value == null)
                return NotFound("Silinecek Hakkımızda Maddesi Bulunamadı");

            await _aboutItemRepository.DeleteAsync(id);
            return Ok("Hakkımızda Maddesi Başarıyla Silindi");
        }
    }
}