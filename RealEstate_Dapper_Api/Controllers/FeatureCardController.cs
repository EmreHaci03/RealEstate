using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.FeatureCardDtos;
using RealEstate_Dapper_Api.Repository.FeatureCardDto;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureCardController : ControllerBase
    {
        private readonly IFeatureCardRepository _featureCardRepository;

        public FeatureCardController(IFeatureCardRepository featureCardRepository)
        {
            _featureCardRepository = featureCardRepository;
        }


        [HttpGet]
        public async Task<IActionResult> FeatureCardList()
        {
            var values = await _featureCardRepository.GetAllAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureCardById(int id)
        {
            var values = await _featureCardRepository.GetByIdAsync(id);
            if(values == null)
                return NotFound("Özellik Kartı Bulunamadı");
            return Ok(values);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateFeatureCard(CreateFeatureCardDto dto)
        {
            await _featureCardRepository.CreateAsync(dto);
            return Ok("Özellik Kartı Eklendi");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeatureCard(int id)
        {
            var values = await _featureCardRepository.GetByIdAsync(id);
            if (values == null)
                return NotFound("Özellik Kartı Bulunamadı");

            await _featureCardRepository.DeleteAsync(id);
            return Ok("Özellik Kartı Başarıyla Silindi");
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateFeatureCard(UpdateFeatureCardDto dto)
        {
            var value = await _featureCardRepository.GetByIdAsync(dto.FeatureCardId);
            if(value == null)
                return NotFound("Özellik Kartı Bulunamadı");

            await _featureCardRepository.UpdateAsync(dto);
            return Ok("Özellik Kartı Başarıyla Güncellendi");
        }
    }
}
