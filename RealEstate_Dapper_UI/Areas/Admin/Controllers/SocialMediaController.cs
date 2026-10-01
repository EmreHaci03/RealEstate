using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.SocialMediaDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SocialMediaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/SocialMedia");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);
                return View(values);
            }

            TempData["ErrorMessage"] = "Sosyal Medyalar yüklenemedi.";
            return View(new List<ResultSocialMediaDto>());
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/SocialMedia", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Sosyal Medya Başarıyla Eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Sosyal Medya Eklenemedi.";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/SocialMedia/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateSocialMediaDto>(jsonData);
                return View(value);
            }

            TempData["ErrorMessage"] = "Sosyal Medya Bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/SocialMedia", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Sosyal Medya Başarıyla Güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Sosyal Medya Güncellenemedi.";
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/SocialMedia/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Sosyal Medya Başarıyla Silindi.";
            else
                TempData["ErrorMessage"] = "Sosyal Medya Silinemedi.";

            return RedirectToAction("Index");
        }
    }
}
