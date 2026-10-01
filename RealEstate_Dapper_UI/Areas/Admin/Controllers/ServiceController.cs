using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ServiceDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Service");
            if (responseMessage.IsSuccessStatusCode) 
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
                return View(values);
            }

            TempData["ErrorMessage"] = "Hizmetler yüklenemedi.";
            return View(new List<ResultServiceDto>());
        }


        [HttpGet]
        public async Task<IActionResult> CreateService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto dto)
        {
            var client= _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content=new StringContent(jsonData,Encoding.UTF8,"application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/Service", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Hizmet başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Hizmet eklenemedi.";
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteService(int id)
        {
            var client=_httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Service/{id}");

            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            else
                TempData["ErrorMessage"] = "Kategori silinemedi.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var client=_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/Service/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();   
                var values=JsonConvert.DeserializeObject<UpdateServiceDto>(jsonData);
                return View(values);
            }
            TempData["ErrorMessage"] = "Biz Kimiz bilgisi bulunamadı.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData=JsonConvert.SerializeObject(dto);
            var content=new StringContent(jsonData, Encoding.UTF8,"application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/Service", content);
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Hizmet bilgisi başarıyla güncellendi.";
            else
                TempData["ErrorMessage"] = "Hizmet bilgisi güncellenemedi.";

            return RedirectToAction("Index");
        }


    }
}
