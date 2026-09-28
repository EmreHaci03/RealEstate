using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.BottomGridDtos;
using RealEstate_Dapper_Api.Repository.BottomGrid;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottomGridController : ControllerBase
    {
        private readonly IBottomGridRepository _bottomGridRepository;

        public BottomGridController(IBottomGridRepository bottomGridRepository)
        {
            _bottomGridRepository = bottomGridRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _bottomGridRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _bottomGridRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Kayıt bulunamadı");

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBottomGridDto dto)
        {
            await _bottomGridRepository.CreateAsync(dto);
            return Ok("Kayıt eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBottomGridDto dto)
        {
            await _bottomGridRepository.UpdateAsync(dto);
            return Ok("Kayıt güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bottomGridRepository.DeleteAsync(id);
            return Ok("Kayıt silindi");
        }
    }
}