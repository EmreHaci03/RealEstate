using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ContactDtos;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ContactController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Contact");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
                return View(values);
            }
            TempData["ErrorMessage"] = "Mesajlar yüklenemedi.";
            return View(new List<ResultContactDto>());
        }

        [HttpGet]
        public async Task<IActionResult> ContactDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetContactByIdDto>(jsonData);
                return View(value);
            }

            TempData["ErrorMessage"] = "Mesaj bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GenerateReply(string name,string subject,string message)
        {
            var prompt =
        "Bir emlak firmasının müşteri temsilcisisin. Aşağıdaki müşteri mesajına Türkçe, kibar, " +
        "kısa ve profesyonel bir e-posta cevabı yaz. Müşteriye adıyla hitap et (Sayın Ad Soyad). " +
        "Fiyat, tarih veya kesin söz gibi bilmediğin bilgileri uydurma; gerekiyorsa " +
        "'danışmanımız en kısa sürede sizinle iletişime geçecek' de. " +
        "Sonunda 'Saygılarımızla,\nEmlak Satış Ekibi' yaz. Sadece cevap metnini döndür.\n\n" +
        $"Müşteri adı: {name}\nKonu: {subject}\nMesaj: {message}";


            var aiClient=_httpClientFactory.CreateClient();
            aiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["OpenAI:ApiKey"]);


            var requestData = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.5
            };


            var jsonData = JsonConvert.SerializeObject(requestData);
            var content=new StringContent(jsonData, Encoding.UTF8,"application/json");
            var responseMessage = await aiClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString=await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
                return BadRequest($"OpenAI hatası ({(int)responseMessage.StatusCode}): {responseString}");


            using var document = JsonDocument.Parse(responseString);
            var answer = document.RootElement.GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Content(answer ?? "");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Mesaj başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Mesaj silinemedi.";

            return RedirectToAction("Index");
        }
    }
}

