using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
