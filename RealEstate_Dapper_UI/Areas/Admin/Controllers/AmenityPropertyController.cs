using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealEstate_Dapper_UI.DTOS.AmenityDtos;
using RealEstate_Dapper_UI.DTOS.AmenityPropertyDtos;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AmenityPropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AmenityPropertyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> GetAmenityPropertyByAmenity(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7025/api/AmenityProperty/GetAmenityPropertiesByAmenity/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAmenityPropertyDto>>(jsonData) ?? new();
                return View(values);
            }
            TempData["ErrorMessage"] = "İlan Olanakları Getirilemedi";
            return View(new List<ResultAmenityPropertyDto>());
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client= _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/AmenityProperty");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAmenityPropertyDto>>(jsonData) ?? new();
                return View(values);
            }
            return View(new List<ResultAmenityPropertyDto>());
        }
        [HttpGet]
        public async Task<IActionResult> CreateAmenityProperty()
        {

            var client=_httpClientFactory.CreateClient();
            var ProductResponse = await client.GetAsync("https://localhost:7025/api/Product/ProductListWithAllDetail");
            if (ProductResponse.IsSuccessStatusCode)
            {
                var jsonData=await ProductResponse.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData);
                ViewBag.Products = new List<SelectListItem> (from x in values
                                                        select new SelectListItem
                                                        {
                                                            Value = x.ProductId.ToString(),
                                                            Text = x.Title
                                                        });

            }
            var AmenityResponse = await client.GetAsync("https://localhost:7025/api/Amenity");
            if (AmenityResponse.IsSuccessStatusCode)
            {
                var AmenityjsonData = await AmenityResponse.Content.ReadAsStringAsync();
                var Amenityvalues = JsonConvert.DeserializeObject<List<ResultAmenityDto>>(AmenityjsonData);
                ViewBag.Amenities = new List<SelectListItem>(from x in Amenityvalues
                                                             select new SelectListItem
                                                             {
                                                                 Value = x.AmenityId.ToString(),
                                                                 Text = x.Title
                                                             });
            }
            return View(new CreateAmenityPropertyDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAmenityProperty(CreateAmenityPropertyDto dto)
        {
            var client=_httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData,Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/AmenityProperty", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "İlan Olanağı Eklendi";
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAmenityProperty(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/AmenityProperty/{id}");
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "İlan Olanağı Başarıyla Silindi";
            else
                TempData["ErrorMessage"] = "İlan Olanağı Silinemedi";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePropertyStatus(int id)
        {
            var client= _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsync($"https://localhost:7025/api/AmenityProperty/ChangeStatus/{id}",null);
            if (responseMessage.IsSuccessStatusCode)
                TempData["SuccessMessage"] = "Durum Değiştirildi";
            else
                TempData["ErrorMessage"] = "Durum Değiştirilemedi";

            return RedirectToAction("Index");
        }
    }
}
