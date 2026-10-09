using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.AmenityDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AmenityController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AmenityController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Amenity");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAmenityDto>>(jsonData) ?? new();
                return View(values);
            }
            TempData["ErrorMessage"] = "Olanaklar Getirilemedi";
            return View(new List<ResultAmenityDto>());
        }

        [HttpGet]
        public IActionResult CreateAmenity()
        {
            return View(new CreateAmenityDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAmenity(CreateAmenityDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/Amenity", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Olanak Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Olanak Eklenirken Hata Oluştu";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAmenity(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/Amenity/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAmenityDto>(jsonData);
                return View(value);
            }
            TempData["ErrorMessage"] = "Olanak Bulunamadı";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAmenity(UpdateAmenityDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/Amenity", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Olanak Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Olanak Güncellenirken Hata Oluştu";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Amenity/{id}");
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Olanak Başarıyla Silindi";
            else
                TempData["ErrorMessage"] = "Olanak Silinemedi";

            return RedirectToAction("Index");
        }
    }
}
