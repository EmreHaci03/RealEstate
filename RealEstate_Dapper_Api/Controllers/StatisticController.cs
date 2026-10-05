using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.DTOS.StatisticDtos;
using RealEstate_Dapper_Api.Repository.Statistic;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticRepository _statisticRepository;

        public StatisticController(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        [HttpGet]
        public async Task<IActionResult> StatisticList()
        {
            var values=await _statisticRepository.GetStatisticsAsync();
            return Ok(values);
        }

        [HttpGet("GetAgentStatistics/{userId}")]
        public async Task<IActionResult> GetAgentStatistics(int userId)
        {
            var values = await _statisticRepository.GetAgentStatisticsAsync(userId);

            if (values == null)
                return Ok(new ResultAgentStatisticDto());

            return Ok(values);
        }

        [HttpGet("GetAgentProductCountByCategory/{userId}")]
        public async Task<IActionResult> GetAgentProductCountByCategory(int userId)
        {
            var values = await _statisticRepository.ResultAgentProductCountByCategoryAsync(userId);
            return Ok(values);
        }

        [HttpGet("GetAveragePriceByCategory")]
        public async Task<IActionResult> GetAveragePriceByCategoryAsync(int userId)
        {
            var values = await _statisticRepository.GetAveragePriceByCategoryAsync(userId);
            return Ok(values);  
        }

        [HttpGet("GetAgentProductCountByCity")]
        public async Task<IActionResult> GetAgentProductCountByCityAsync(int userId)
        {
            var values=await _statisticRepository.GetAgentProductCountByCityAsync(userId);
            return Ok(values);
        }
    }
}

