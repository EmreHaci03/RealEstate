using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.ProductImageDtos;
using RealEstate_Dapper_Api.Repository.ProductImage;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageController(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ProductImageList()
        {
            var values = await _productImageRepository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageById(int id)
        {
            var values=await _productImageRepository.GetProductImageByIdAsync(id);
            if (values == null)
                return NotFound("Resim Bulunamadı");
            return Ok(values);
        }
        
        [HttpGet("GetProductImageByProductId/{ProductId}")]
        public async Task<IActionResult> GetProductImageByProductAsync(int ProductId)
        {
            var values=await _productImageRepository.GetProductImageByProductAsync(ProductId);
            if (values == null)
                return NotFound("Resim Bulunamadı");
            
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto dto)
        {
            await _productImageRepository.CreateAsync(dto);
            return Ok("Resim Başarıyla Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var values = await _productImageRepository.GetProductImageByIdAsync(id);
            if (values == null)
                return NotFound("Silinecek Resim Bulunamadı");

            await _productImageRepository.DeleteAsync(id);
            return Ok("Resim Başarıyla Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto dto)
        {
            await _productImageRepository.UpdateAsync(dto);
            return Ok("Resim Güncellendi");
        }
    }

}
