using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.AdressDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdressController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdressController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Adress");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultAdressDto>>(jsonData);
                return View(values);
            }
            TempData["ErrorMessage"] = "Adres yüklenemedi.";
            return View(new List<ResultAdressDto>());
        }

        [HttpGet]
        public async Task<IActionResult> CreateAdress()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdress(CreateAdressDto dto)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/Adress", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Adres başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Adres eklenemedi.";
            return RedirectToAction("CreateAdress");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdress(int id)
        {
            var client=_httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Adress/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Adres başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Adres silinemedi.";

            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> UpdateAdress(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/Adress/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<UpdateAdressDto>(jsonData);
                return View(values);
            }

            TempData["ErrorMessage"] = "Adres bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdress(UpdateAdressDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/Adress", content);
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Adres başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Adres silinemedi.";

            return RedirectToAction("Index");
        }
    }
}
