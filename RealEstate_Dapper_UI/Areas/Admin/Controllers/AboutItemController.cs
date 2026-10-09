using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.AboutDtos;
using RealEstate_Dapper_UI.DTOS.AboutItemDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AboutItemController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutItemController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAboutItem(int id, int aboutId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/AboutItem/{id}");
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Hakkımızda Maddesi Başarıyla Silindi";
            else
                TempData["ErrorMessage"] = "Hakkımızda Maddesi Silinemedi";

            return RedirectToAction("Index", "About", new {area="Admin",id= aboutId });
        }

        [HttpGet]
        public async Task<IActionResult> CreateAboutItem()
        {
            var client = _httpClientFactory.CreateClient();
            var aboutResponse = await client.GetAsync("https://localhost:7025/api/About");
            if (aboutResponse.IsSuccessStatusCode)
            {
                var aboutJson = await aboutResponse.Content.ReadAsStringAsync();
                var abouts = JsonConvert.DeserializeObject<List<ResultAboutDto>>(aboutJson) ?? new();
                ViewBag.Abouts = abouts.Select(x => new SelectListItem
                {
                    Value = x.AboutId.ToString(),
                    Text = x.Title
                }).ToList();
            }
            return View(new CreateAboutItemDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutItem(CreateAboutItemDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/AboutItem", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Hakkımızda Maddesi Başarıyla Eklendi";
                return RedirectToAction("AboutItems", "About", new { id = dto.About });
            }
            ViewBag.ErrorMessage = "Madde Eklenirken Hata Oluştu";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAboutItem(int id, int aboutId)
        {
            var client = _httpClientFactory.CreateClient();

            var aboutResponse = await client.GetAsync("https://localhost:7025/api/About");
            if (aboutResponse.IsSuccessStatusCode)
            {
                var aboutJson = await aboutResponse.Content.ReadAsStringAsync();
                var abouts = JsonConvert.DeserializeObject<List<ResultAboutDto>>(aboutJson) ?? new();
                ViewBag.Abouts = abouts.Select(x => new SelectListItem
                {
                    Value = x.AboutId.ToString(),
                    Text = x.Title
                }).ToList();
            }

            var responseMessage = await client.GetAsync($"https://localhost:7025/api/AboutItem/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<GetAboutItemByIdDto>(jsonData);
                var dto = new UpdateAboutItemDto
                {
                    AboutItemId = item!.AboutItemId,
                    Icon = item.Icon,
                    Title = item.Title,
                    Content = item.Content,
                    About = aboutId
                };
                return View(dto);
            }

            TempData["ErrorMessage"] = "Madde Bulunamadı";
            return RedirectToAction("AboutItems", "About", new { id = aboutId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAboutItem(UpdateAboutItemDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/AboutItem", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Hakkımızda Maddesi Başarıyla Güncellendi";
                return RedirectToAction("AboutItems", "About", new { id = dto.About });
            }
            ViewBag.ErrorMessage = "Madde Güncellenirken Hata Oluştu";
            return View(dto);
        }
    }
}
