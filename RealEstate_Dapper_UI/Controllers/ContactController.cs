using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.AdressDtos;
using RealEstate_Dapper_UI.DTOS.ContactDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7025/api/Adress");

            ResultAdressDto? adress = null;

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAdressDto>>(json);
                adress = values?.FirstOrDefault();
            }

            return View(adress);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7025/api/Contact", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["ContactSuccess"] = "Mesajınız başarıyla gönderildi. En kısa sürede size dönüş yapacağız.";
            }
            else
            {
                TempData["ContactError"] = "Mesajınız gönderilemedi. Lütfen daha sonra tekrar deneyin.";
            }

            return RedirectToAction("Index");
        }
    }
}