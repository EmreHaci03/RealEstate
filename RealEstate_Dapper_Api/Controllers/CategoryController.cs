using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.CategoryDtos;
using RealEstate_Dapper_Api.Repository.Category;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await categoryRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var value = await categoryRepository.GetCategoryByIdAsync(id);
            if (value == null)
                return NotFound("Kategori bulunamadı");

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            await categoryRepository.CreateAsync(dto);
            return Ok("Kategori Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            await categoryRepository.UpdateAsync(dto);
            return Ok("Kategori Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
                return BadRequest("Geçersiz kategori Id'si");

            var category = categoryRepository.GetCategoryByIdAsync(id);
            if(category==null)
                return NotFound("Kategori bulunamadı");

            await categoryRepository.DeleteAsync(id);
            return Ok("Kategori başarıyla silindi");
        }
    }
}
