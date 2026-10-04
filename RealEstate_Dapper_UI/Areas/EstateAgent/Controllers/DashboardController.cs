using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.StatisticDtos;
using System.Security.Claims;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    [Authorize(Roles ="Member")]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var client = _httpClientFactory.CreateClient();

            var stats = new ResultAgentStatisticDto();
            ViewBag.CategoryName = "[]";
            ViewBag.ProductCount = "[]";

            // 1. İstatistikler
            var statResponse = await client.GetAsync($"https://localhost:7025/api/Statistic/GetAgentStatistics/{userId}");
            if (statResponse.IsSuccessStatusCode)
            {
                var jsonData = await statResponse.Content.ReadAsStringAsync();
                stats = JsonConvert.DeserializeObject<ResultAgentStatisticDto>(jsonData) ?? new();
            }

            // 2. Kategori dağılımı
            var categoryResponse = await client.GetAsync($"https://localhost:7025/api/Statistic/GetAgentProductCountByCategory/{userId}");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var jsonData = await categoryResponse.Content.ReadAsStringAsync();
                var categoryValues = JsonConvert.DeserializeObject<List<ResultAgentProductCountByCategoryDto>>(jsonData) ?? new();

                ViewBag.CategoryName = JsonConvert.SerializeObject(categoryValues.Select(x => x.CategoryName));
                ViewBag.ProductCount = JsonConvert.SerializeObject(categoryValues.Select(x => x.ProductCount));
            }

            return View(stats);
        }
    }
}

