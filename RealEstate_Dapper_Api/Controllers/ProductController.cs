using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.ProductDtos;
using RealEstate_Dapper_Api.Repository.Employee;
using RealEstate_Dapper_Api.Repository.Product;
using System.Security.Claims;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IEmployeeRepository employeeRepository;

        public ProductController(IProductRepository productRepository, IEmployeeRepository employeeRepository)
        {
            this.productRepository = productRepository;
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values=await productRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("GetProductWithCount")]
        public async Task<IActionResult> GetProductWithCount()
        {
            var values = await productRepository.ResultProductWithCityCount();
            return Ok(values);
        }


        [HttpGet("ResultProductWithSearch/{searchKeyValue}/{categoryId}/{city}")]
        public async Task<IActionResult> ResultProductWithSearch(string? searchKeyValue, int? categoryId, string? city)
        {
            var values = await productRepository.ResultProductWithSearchList(searchKeyValue, categoryId, city);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var value = await productRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Emlak Bilgisi Bulunamadı!");

            return Ok(value);
        }

        [HttpGet("GetProductAdvertsListByUser/{userId}")]
        public async Task<IActionResult> GetProductAdvertsListByUser(int userId)
        {
            var values = await productRepository.GetProductAdvertsListByUserAsync(userId);
            return Ok(values);
        }

        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await productRepository.ResultProductWithCategoryAsync();
            if (values == null)
                return NotFound("Emlak Bilgisi Alınamadı!");
            return Ok(values);
        }


        [HttpGet("ProductListWithAllDetail")]
        public async Task<IActionResult> ProductListWithDetail()
        {
            var values = await productRepository.ResultProductWithCategoryAndEmployeeAsync();
            if (values == null)
                return NotFound("Emlak Bilgisi Alınamadı!");


            return Ok(values);
        }

        [HttpGet("ProductAdvertsListByEmployee")]
        public async Task<IActionResult> ProductAdvertsListByEmployee(int id)
        {
            var values = await productRepository.GetProductAdvertsListByEmployeeAsync(id);
            return Ok(values);
        }


        [HttpGet("GetAdvertDetailByUser/{userId}/{productId}")]
        public async Task<IActionResult> GetAdvertDetailByUser(int productId, int userId)
        {
            var values = await productRepository.GetAdvertDetailByUserAsync(productId,userId);
            if (values == null)
                return NotFound("Emlak Detay Bilgisi Alınamadı");

            return Ok(values);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            await productRepository.CreateAsync(dto);
            return Ok("Emlak Başarıyla Eklendi");
        }

        [HttpPost("CreateAdvertByUser/{userId}")]
        public async Task<IActionResult> CreateAdvertByUser(int userId, CreateProductDto dto)
        {
            var success = await productRepository.CreateAdvertByUserAsync(dto, userId);
            if (!success)
                return BadRequest("Bu kullanıcıya bağlı danışman bulunamadı");

            return Ok("İlan başarıyla eklendi");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound("Emlak Bilgisi Bulunamadı!");

            await productRepository.DeleteAsync(id);
            return Ok("Emlak Başarıyla Silindi");
        }

        [HttpDelete("DeleteAdvertByUser/{userId}/{productId}")]
        public async Task<IActionResult> DeleteAdvertByUser(int userId, int productId)
        {
            var success = await productRepository.DeleteAdvertByUserAsync(userId, productId);
            if (!success)
                return NotFound("İlan bulunamadı ya da bu ilanı silme yetkiniz yok");

            return Ok("İlanınız başarıyla silindi");
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            await productRepository.UpdateAsync(dto);
            return Ok("Emlak Başarıyla Güncellendi");
        }

        [HttpPut("UpdateAdvertByUser/{userId}")]
        public async Task<IActionResult> UpdateAdvertByUserAsync(UpdateAdvertDto dto,int userId)
        {
            if (dto.Category <= 0)
                return BadRequest("Lütfen bir kategori seçin");

            var success = await productRepository.UpdateAdvertByUserAsync(dto, userId);
            if (!success)
                return NotFound("İlan bulunamadı ya da bu ilanı düzenleme yetkiniz yok");

            return Ok("İlan başarıyla güncellendi");
        }



    }
}
