using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.CategoryDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Category");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }

            TempData["ErrorMessage"] = "Kategoriler yüklenemedi.";
            return View(new List<ResultCategoryDto>());
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/Category", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kategori başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Kategori eklenemedi.";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/Category/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(value);
            }

            TempData["ErrorMessage"] = "Kategori bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/Category", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kategori başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Kategori güncellenemedi.";
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Category/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Kategori silinemedi.";

            return RedirectToAction("Index");
        }
    }
}