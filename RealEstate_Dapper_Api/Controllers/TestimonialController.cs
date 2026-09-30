using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.TestimonialDtos;
using RealEstate_Dapper_Api.Repository.Testimonial;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialRepository _testimonialRepository;

        public TestimonialController(ITestimonialRepository testimonialRepository)
        {
            _testimonialRepository = testimonialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> TestimonialList()
        {
            var values = await _testimonialRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("Active")]
        public async Task<IActionResult> GetActiveTestimonials()
        {
            var values = await _testimonialRepository.GetActiveTestimonialsAsync();
            return Ok(values);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestimonialById(int id)
        {
            var value = await _testimonialRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Yorum bulunamadı");

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto dto)
        {
            await _testimonialRepository.CreateAsync(dto);
            return Ok("Yorum başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto dto)
        {
            var value = await _testimonialRepository.GetByIdAsync(dto.TestimonialId);
            if (value == null)
                return NotFound("Yorum bulunamadı");

            await _testimonialRepository.UpdateAsync(dto);
            return Ok("Yorum başarıyla güncellendi");
        }

        [HttpPut("ChangeTestimonialStatus")]
        public async Task<IActionResult> ChangeStatus(int id,bool status)
        {
            var subscriber = await _testimonialRepository.GetByIdAsync(id);
            if (subscriber == null)
                return NotFound("Abone bulunamadı");

            await _testimonialRepository.ChangeTestimonialStatus(id, status);
            return Ok(status ? "Yorum Aktif Edildi" : "Yorum Pasif Edildi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var value = await _testimonialRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Yorum bulunamadı");

            await _testimonialRepository.DeleteAsync(id);
            return Ok("Yorum başarıyla silindi");
        }
    }
}