using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.SubscriberDtos;
using System.Net.Http.Headers;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SubscriberController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubscriberController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]

        public async Task<IActionResult>  Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Subscriber");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSubscriberDto>>(jsonData);
                return View(values);
            }
            TempData["ErrorMessage"] = "Bülten Abone Listesi Alınamadı";
            return View(new List<ResultSubscriberDto>());
        }


        [HttpPost]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = User.FindFirst("AccessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Subscriber/{id}");
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Abone Başarıyla Silindi.";
            else
                TempData["ErrorMessage"] = "Abone Silinemedi.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSubscriberStatus(int? id ,bool isActive)
        {
            var client= _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsync($"https://localhost:7025/api/Subscriber/ChangeStatus/{id}?isActive={isActive}", null);

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = isActive ? "Abone aktif edildi." : "Abone pasif edildi.";
            else
                TempData["ErrorMessage"] = "Abone durumu değiştirilemedi.";

            return RedirectToAction("Index");
        }
    }
}
