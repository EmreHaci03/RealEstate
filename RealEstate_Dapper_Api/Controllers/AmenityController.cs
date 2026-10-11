using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.AmenityDtos;
using RealEstate_Dapper_Api.Repository.Amenity;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityRepository _amenityRepository;

        public AmenityController(IAmenityRepository amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }
        [HttpGet]
        public async Task<IActionResult> AmenityList()
        {
            var values = await _amenityRepository.GetAllAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmenityById(int id)
        {
            var values = await _amenityRepository.GetByIdAsync(id);
            if (values == null)
                return NotFound("Oda Olanağı Bulunamadı");
            return Ok(values);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAmenity(CreateAmenityDto dto)
        {
            await _amenityRepository.CreateAsync(dto);
            return Ok("Oda Olanakları Eklendi");
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAmenity(UpdateAmenityDto dto)
        {
            var values = await _amenityRepository.GetByIdAsync(dto.AmenityId);
            if (values == null)
                return NotFound("Oda Olanağı Bulunamadı");

            await _amenityRepository.UpdateAsync(dto);
            return Ok("Oda Olanağı Güncellendi");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            var values = await _amenityRepository.GetByIdAsync(id);
            if (values == null)
                return NotFound("Oda Olanağı Bulunamadı");

            await _amenityRepository.DeleteAsync(id);
            return Ok("Oda Olanağı Silindi");
        }

    }
}
