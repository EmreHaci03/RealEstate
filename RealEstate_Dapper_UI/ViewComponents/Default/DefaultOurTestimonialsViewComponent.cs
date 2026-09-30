using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ClientDtos;
using RealEstate_Dapper_UI.DTOS.TestimonialDtos;

namespace RealEstate_Dapper_UI.ViewComponents.Default
{
    public class DefaultOurTestimonialsViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultOurTestimonialsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7025/api/Testimonial/Active");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
