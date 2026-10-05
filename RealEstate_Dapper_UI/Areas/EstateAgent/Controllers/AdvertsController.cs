using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.CategoryDtos;
using RealEstate_Dapper_UI.DTOS.ProductDetailDtos;
using RealEstate_Dapper_UI.DTOS.ProductDtos;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    [Authorize(Roles = "Member")]
    public class AdvertsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdvertsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // Giriş yapan kullanıcının id'si
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account", new { area = "" });

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7025/api/Product/GetProductAdvertsListByUser/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(jsonData);
                return View(values);
            }

            ViewBag.Error = $"API hatası: {(int)response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            return View(new List<ResultProductWithDetailDto>());


        }


        [HttpGet]
        public async Task<IActionResult> CreateAdvert()
        {

            var client=_httpClientFactory.CreateClient();
            var CategoryresponseMessage = await client.GetAsync("https://localhost:7025/api/Category");
            if (CategoryresponseMessage.IsSuccessStatusCode)
            {
                var jsonData = await CategoryresponseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                List<SelectListItem> categoryValues = (from x in values.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Value=x.CategoryId.ToString(),
                                                       Text=x.CategoryName
                                                   }).ToList();
                ViewBag.Categories= categoryValues;

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvert(CreateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var UserId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            dto.Employee = int.Parse(UserId);
            var jsonData=JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7025/api/Product/CreateAdvertByUser/{UserId}",content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "İlan Başarıyla Eklendi";
                return RedirectToAction("Index");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var client= _httpClientFactory.CreateClient();
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var responseMessage = await client.DeleteAsync($"https://localhost:7025/api/Product/DeleteAdvertByUser/{UserId}/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "İlan Başarıyla Silindi";
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateAdvert(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var categoryResponse = await client.GetAsync("https://localhost:7025/api/Category");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var jsonData=await categoryResponse.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                List<SelectListItem> CategoryValues=(from x in values.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Value=x.CategoryId.ToString(),
                                                         Text=x.CategoryName,
                                                     }).ToList();
                ViewBag.Categories = CategoryValues;    
            }

            var ProductResponse = await client.GetAsync($"https://localhost:7025/api/Product/GetAdvertDetailByUser/{userId}/{id}");
            if(ProductResponse.IsSuccessStatusCode)
            {
                var jsonData=await ProductResponse.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);   
                return View(values);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdvert(UpdateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var jsonData = JsonConvert.SerializeObject(dto);
            var content=new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7025/api/Product/UpdateAdvertByUser/{userId}", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "İlan Güncellendi";
                return RedirectToAction("Index");
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> AdvertDetail(int id)
        {
            var client= _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync($"https://localhost:7025/api/ProductDetail/GetProductDetailByProductId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData);
                return View(values);
            }
            return View(new GetProductDetailByIdDto());
        }

        [HttpGet]
        public async Task<IActionResult> CreateAdvertDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync($"https://localhost:7025/api/ProductDetail/GetProductDetailByProductId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Mülkünüzün Detayı Mevcuttur";
                return RedirectToAction("Index");
            }

            return View(new CreateProductDetailDto { Product = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvertDetail(CreateProductDetailDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/ProductDetail", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "İlan özellikleri eklendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Özellikler eklenemedi: " + await responseMessage.Content.ReadAsStringAsync();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAdvertDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7025/api/ProductDetail/GetProductDetailByProductId/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Bu ilan için özellik kaydı yok, önce özellik ekleyin.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var detail = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData)!;   // Product burada string

            var model = new UpdateProductDetailDto
            {
                ProductDetailId = detail.ProductDetailId,
                ProductSize = detail.ProductSize,
                BedRoomCount = detail.BedRoomCount,
                BathCount = detail.BathCount,
                RoomCount = detail.RoomCount,
                GarageSize = detail.GarageSize,
                BuildYear = detail.BuildYear,
                Price = detail.Price,
                Location = detail.Location,
                VideoUrl = detail.VideoUrl,
                Product = id                 // ilanın id'si adresten
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdvertDetail(UpdateProductDetailDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7025/api/ProductDetail", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "İlan özellikleri güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Özellikler güncellenemedi: " + await response.Content.ReadAsStringAsync();
            return View(dto);
        }
    }
}