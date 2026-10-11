using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.WhoWeAreDetailDtos;
using System.Net.Http.Headers;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class WhoWeAreDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WhoWeAreDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/WhoWeAreDetail");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData);
                return View(values);
            }

            TempData["ErrorMessage"] = "Biz Kimiz bilgileri yüklenemedi.";
            return View(new List<ResultWhoWeAreDetailDto>());
        }

        [HttpGet]
        public IActionResult CreateWhoWeAreDetail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhoWeAreDetail(CreateWhoWeAreDetailDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = User.FindFirst("AccessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/WhoWeAreDetail", content);

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Biz Kimiz bilgisi başarıyla eklendi.";
            else
                TempData["ErrorMessage"] = "Biz Kimiz bilgisi eklenemedi.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateWhoWeAreDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/WhoWeAreDetail/GetById/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateWhoWeAreDetailDto>(jsonData);
                return View(value);
            }

            TempData["ErrorMessage"] = "Biz Kimiz bilgisi bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWhoWeAreDetail(UpdateWhoWeAreDetailDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = User.FindFirst("AccessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/WhoWeAreDetail", content);

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Biz Kimiz bilgisi başarıyla güncellendi.";
            else
                TempData["ErrorMessage"] = "Biz Kimiz bilgisi güncellenemedi.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWhoWeAreDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = User.FindFirst("AccessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/WhoWeAreDetail/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Biz Kimiz bilgisi başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Biz Kimiz bilgisi silinemedi.";

            return RedirectToAction("Index");
        }
    }
}