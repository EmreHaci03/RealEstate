using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.WhoWeAreDtos;
using RealEstate_Dapper_Api.Repository.WhoWeAre;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhoWeAreDetailController : ControllerBase
    {
        private readonly IWhoWeAreRepository repository;

        public WhoWeAreDetailController(IWhoWeAreRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> WhoWeAreDetailList()
        {
            var values = await repository.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetWhoWeAreDetailById(int id)
        {
            var values = await repository.GetByIdAsync(id);
            if(values == null)
                return NotFound("Belirtilen Alan Bulunamadı");  

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhoWeAreDetail(CreateWhoWeAreDetailDto dto)
        {
            await repository.CreateAsync(dto);
            return Ok("Biz Kimiz Alanı Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhoWeAreDetail(int id)
        {
            var values=await repository.GetByIdAsync(id);   
            if(values == null)
                return NotFound("Belirtilen Alan Bulunamadı");

            await repository.DeleteAsync(id);
            return Ok("Biz Kimiz Alanı Başarıyla Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWhoWeAreDetail(UpdateWhoWeAreDetailDto dto)
        {
            await repository.UpdateAsync(dto);
            return Ok("Biz Kimiz Alanı Güncellendi");
        }
    }
}
