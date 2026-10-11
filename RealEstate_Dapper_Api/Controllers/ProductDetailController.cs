using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.ProductDetailDtos;
using RealEstate_Dapper_Api.Repository.ProductDetailDto;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailRepository productDetailRepository;

        public ProductDetailController(IProductDetailRepository productDetailRepository)
        {
            this.productDetailRepository = productDetailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetailList()
        {
            var values = await productDetailRepository.GetAllAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(int id)
        {
            var values = await productDetailRepository.GetByIdAsync(id);
            if (values == null)
                return NotFound("Ürün Detay Bilgisi Alınamadı");
            return Ok(values);
        }

        [HttpGet("GetProductDetailByProductId/{ProductId}")]
        public async Task<IActionResult> GetProductDetailByProductId(int ProductId)
        {
            var values=await productDetailRepository.GetProductDetailByProductId(ProductId);
            if (values == null)
                return NotFound("Emlak Detayı Bulunamadı");

            return Ok(values);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto dto)
        {
            await productDetailRepository.CreateAsync(dto);
            return Ok("Ürün Detayı Başarıyla Eklendi");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            var values=await productDetailRepository.GetByIdAsync(id);
            if (values == null)
                return NotFound("Ürün Detayı Bulunamadı");
            await productDetailRepository.DeleteAsync(id);
            return Ok("Ürün Detayı Silindi");
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto dto)
        {
            if (dto.ProductDetailId == null)
                return NotFound("Ürün Detay Bilgisi Alınamadı");

            await productDetailRepository.UpdateAsync(dto);
            return Ok("Ürün Detayı Güncellendi");
        }
    }
}
