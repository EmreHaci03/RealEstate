using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.AdressDtos;
using RealEstate_Dapper_Api.Repository.Adress;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        private readonly IAdressRepository _adressRepository;

        public AdressController(IAdressRepository adressRepository)
        {
            _adressRepository = adressRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdress()
        {
            var values = await _adressRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdressById(int id)
        {
            var value = await _adressRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Adres bulunamadı");

            return Ok(value);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAdress(CreateAdressDto dto)
        {
            await _adressRepository.CreateAsync(dto);
            return Ok("Adres başarıyla eklendi");
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAdress(UpdateAdressDto dto)
        {
            var value = await _adressRepository.GetByIdAsync(dto.AdressId);
            if (value == null)
                return NotFound("Adres bulunamadı");

            await _adressRepository.UpdateAsync(dto);
            return Ok("Adres başarıyla güncellendi");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdress(int id)
        {
            var value = await _adressRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Adres bulunamadı");

            await _adressRepository.DeleteAsync(id);
            return Ok("Adres başarıyla silindi");
        }
    }
}