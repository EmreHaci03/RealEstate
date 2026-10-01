using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.FeatureCardDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureCardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureCardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/FeatureCard");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureCardDto>>(jsonData);
                return View(values);
            }
            TempData["ErrorMessage"] = "Özellik Kartları yüklenemedi.";
            return View(new List<ResultFeatureCardDto>());
        }

        [HttpGet]
        public async Task<IActionResult> CreateFeatureCard()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeatureCard(CreateFeatureCardDto dto)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/FeatureCard", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Özellik Kartları başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Özellik Kartları eklenemedi.";
            return RedirectToAction("CreateFeatureCard");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFeatureCard(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/FeatureCard/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Özellik Kartları başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Özellik Kartları silinemedi.";

            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> UpdateFeatureCard(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/FeatureCard/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureCardDto>(jsonData);
                return View(values);
            }

            TempData["ErrorMessage"] = "Özellik Kartları bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeatureCard(UpdateFeatureCardDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/FeatureCard", content);
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Özellik kartı başarıyla güncellendi.";
            else
                TempData["ErrorMessage"] = "Özellik kartı güncellenemedi.";

            return RedirectToAction("Index");
        }
    }
}
