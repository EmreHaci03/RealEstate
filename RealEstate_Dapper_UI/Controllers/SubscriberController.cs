using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.SubscriberDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubscriberController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async  Task<IActionResult> CreateSubscriber(CreateSubscriberDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7025/api/Subscriber", content);
            var message = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                TempData["SubscribeSuccess"] = message;
            else
                TempData["SubscribeError"] = message;

            return RedirectToAction("Index", "Default");
        }
    }
}
