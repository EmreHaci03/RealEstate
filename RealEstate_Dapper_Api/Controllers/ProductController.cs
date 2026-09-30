using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.ProductDtos;
using RealEstate_Dapper_Api.Repository.Product;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var value = await productRepository.GetByIdAsync(id);
            if (value == null)
                return NotFound("Emlak Bilgisi Bulunamadı!");

            return Ok(value);
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            await productRepository.CreateAsync(dto);
            return Ok("Emlak Başarıyla Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound("Emlak Bilgisi Bulunamadı!");

            await productRepository.DeleteAsync(id);
            return Ok("Emlak Başarıyla Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            await productRepository.UpdateAsync(dto);
            return Ok("Emlak Başarıyla Güncellendi");
        }

    }
}
