using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.BottomGridDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BottomGridController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BottomGridController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/BottomGrid");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBottomGridDto>>(jsonData);
                return View(values);
            }
            TempData["ErrorMessage"] = "Hızlı Erişim Kartları Yüklenemedi.";
            return View(new List<ResultBottomGridDto>());
        }

        [HttpGet]
        public async Task<IActionResult> CreateBottomGrid()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBottomGrid(CreateBottomGridDto dto)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/BottomGrid", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Hızlı Erişim Kartları Başarıyla Eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Hızlı Erişim Kartları Eklenemedi.";
            return RedirectToAction("CreateBottomGrid");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBottomGrid(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/BottomGrid/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Hızlı Erişim Kartları Başarıyla Silindi.";
            else
                TempData["ErrorMessage"] = "Hızlı Erişim Kartları Silinemedi.";

            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> UpdateBottomGrid(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/BottomGrid/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBottomGridDto>(jsonData);
                return View(values);
            }

            TempData["ErrorMessage"] = "Hızlı Erişim Kartları bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBottomGrid(UpdateBottomGridDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/BottomGrid", content);
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Hızlı Erişim Kartları Başarıyla Silindi.";
            else
                TempData["ErrorMessage"] = "Hızlı Erişim Kartları Silinemedi.";

            return RedirectToAction("Index");
        }
    }
}
