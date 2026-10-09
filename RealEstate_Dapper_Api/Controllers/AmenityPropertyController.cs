using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.AmenityPropertyDtos;
using RealEstate_Dapper_Api.Repository.AmenityProperty;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityPropertyController : ControllerBase
    {
        private readonly IAmenityPropertyRepository _PropertyRepository;

        public AmenityPropertyController(IAmenityPropertyRepository propertyRepository)
        {
            _PropertyRepository = propertyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AmenityPropertyList()
        {
            var values = await _PropertyRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmenityProperty(int id)
        {
            var value = await _PropertyRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("İlan Olanağı Bulunamadı");
            return Ok(value);
        }



        [HttpGet("GetTrueAmenityPropertByProduct/{productId}")]
        public async Task<IActionResult> GetTrueAmenityPropertByProduct(int productId)
        {
            var values = await _PropertyRepository.GetTrueAmenityPropertByProductAsync(productId);
            return Ok(values);
        }

        [HttpGet("GetAmenityPropertiesByAmenity/{amenityId}")]
        public async Task<IActionResult> GetAmenityPropertiesByAmenity(int amenityId)
        {
            var values = await _PropertyRepository.GetAmenityPropertByAmenityAsync(amenityId);
            return Ok(values);
        }

        [HttpGet("GetAmenityPropertiesByProduct/{productId}")]
        public async Task<IActionResult> GetAmenityPropertiesByProduct(int productId)
        {
            var values = await _PropertyRepository.GetAmenityPropertByProductAsync(productId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAmenityProperty(CreateAmenityPropertyDto dto)
        {
            await _PropertyRepository.CreateAsync(dto);
            return Ok("İlan Olanağı Başarıyla Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAmenityProperty(UpdateAmenityPropertyDto dto)
        {
            await _PropertyRepository.UpdateAsync(dto);
            return Ok("İlan Olanağı Başarıyla Güncellendi");
        }
        [HttpPut("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var success = await _PropertyRepository.AmenityPropertyChangeStatus(id);
            if (!success)
                return NotFound("İlan Olanağı Bulunamadı");
            return Ok("Durum Değiştirildi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenityProperty(int id)
        {
            var value = await _PropertyRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Silinecek İlan Olanağı Bulunamadı");

            await _PropertyRepository.DeleteAsync(id);
            return Ok("İlan Olanağı Başarıyla Silindi");
        }
    }
}