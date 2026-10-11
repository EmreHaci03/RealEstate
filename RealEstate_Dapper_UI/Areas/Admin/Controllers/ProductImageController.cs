using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using RealEstate_Dapper_UI.DTOS.ProductImageDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/ProductImage");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(jsonData) ?? new();
                return View(values);
            }
            return View(new List<ResultProductImageDto>());
        }

        [HttpGet]
        public async Task<IActionResult> CreateProductImage()
        {
            await LoadProductsAsync();
            return View(new CreateProductImageDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/ProductImage", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Resim Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            await LoadProductsAsync();
            TempData["ErrorMessage"] = "Resim Eklenirken Hata Oluştu";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/ProductImage/{id}");
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Resim Başarıyla Silindi";
            else
                TempData["ErrorMessage"] = "Resim Silinirken Hata Oluştu";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductImage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/ProductImage/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductImageDto>(jsonData);
                await LoadProductsAsync();
                return View(values);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7025/api/ProductImage", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Resim Güncellendi";
                return RedirectToAction("Index");
            }
            await LoadProductsAsync();
            return View(dto);
        }

        private async Task LoadProductsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Product");
            var products = new List<ResultProductDto>();
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData) ?? new();
            }
            ViewBag.Products = products.Select(x => new SelectListItem
            {
                Value = x.ProductId.ToString(),
                Text = x.Title
            }).ToList();
        }
    }
}