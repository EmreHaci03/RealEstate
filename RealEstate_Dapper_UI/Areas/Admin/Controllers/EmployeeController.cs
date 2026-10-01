using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.EmployeeDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Employee");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEmployeeDto>>(jsonData);
                return View(values);
            }

            TempData["ErrorMessage"] = "Danışmanlar yüklenemedi.";
            return View(new List<ResultEmployeeDto>());
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/Employee", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Danışman başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Danışman eklenemedi.";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/Employee/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateEmployeeDto>(jsonData);
                return View(value);
            }

            TempData["ErrorMessage"] = "Danışman bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/Employee", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Danışman başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Danışman güncellenemedi.";
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Employee/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Danışman başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Danışman silinemedi.";

            return RedirectToAction("Index");
        }
    }
}