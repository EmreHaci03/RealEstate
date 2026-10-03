using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repository.Dashboard;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet("DashboardSummary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var values = await _dashboardRepository.GetSummaryAsync();
            return Ok(values);
        }

        [HttpGet("GetAveragePriceByCategory")]
        public async Task<IActionResult> GetAveragePriceByCategory()
        {
            var values = await _dashboardRepository.GetAveragePriceByCategory();
            return Ok(values);
        }
        
        [HttpGet("GetEmployeePerformance")]
        public async Task<IActionResult> GetEmployeePerformance()
        {
            var values = await _dashboardRepository.GetEmployeePerformance();
            return Ok(values);
        }

        [HttpGet("GetProductCountByCategory")]
        public async Task<IActionResult> GetProductCountByCategoryAsync()
        {
            var values = await _dashboardRepository.GetProductCountByCategoryAsync();
            return Ok(values);
        }

        [HttpGet("GetProductByCity")]
        public async Task<IActionResult> GetProductByCity()
        {
            var values = await _dashboardRepository.GetProductCountByCityAsync();
            return Ok(values);
        }

        [HttpGet("GetDashboardDayMessage")]
        public async Task<IActionResult> GetDashboardDayMessage()
        {
            var values = await _dashboardRepository.GetDashboardDayMessageAsync();
            return Ok(values);
        }
    }
}
