using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.AboutDtos;
using RealEstate_Dapper_UI.DTOS.AboutItemDtos;
using System.Net.Http.Headers;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/About");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }
            TempData["ErrorMessage"] = "Hakkımızda Bulunamadı";
            return View(new List<ResultAboutDto>());
        }

        [HttpGet]
        public async Task<IActionResult> AboutItems(int id)
        {
            var client=_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/AboutItem/GetAboutItemsByAbout/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutItemDto>>(jsonData);
                return View(values);
            }
            TempData["ErrorMessage"] = "Hakkımızda Öğeleri Bulunamadı";
            return View(new List<ResultAboutItemDto>());
        }
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View(new CreateAboutDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = User.FindFirst("AccessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/About", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Hakkımızda Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Hakkımızda Eklenirken Hata Oluştu";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/About/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);
                return View(value);
            }
            TempData["ErrorMessage"] = "Hakkımızda Bulunamadı";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = User.FindFirst("AccessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/About", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Hakkımızda Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Hakkımızda Güncellenirken Hata Oluştu";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = User.FindFirst("AccessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/About/{id}");
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Hakkımızda Başarıyla Silindi";
            else
                TempData["ErrorMessage"] = "Silinemedi, Bağlı Maddeler Olabilir";

            return RedirectToAction("Index");
        }
    }
}
