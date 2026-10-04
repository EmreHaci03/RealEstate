using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ProductDtos;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    [Authorize(Roles = "Member")]
    public class AdvertsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdvertsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // Giriş yapan kullanıcının id'si
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7025/api/Product/GetProductAdvertsListByUser/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData);
                return View(values);
            }

            ViewBag.Error = $"API hatası: {(int)response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            return View(new List<ResultProductWithDetailDto>());
        }
    }
}