using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.ProductDtos;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


[Area("EstateAgent")]
[Authorize]
public class AiAssistantController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public AiAssistantController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult AskAI()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AskAI(string message)
    {
        ViewBag.Message = message;

        var client = _httpClientFactory.CreateClient();
        var productResponse = await client.GetAsync("https://localhost:7025/api/Product/ProductListWithCategory");
        var productJson = await productResponse.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<List<ResultProductWithDetailDto>>(productJson);

        var shortList = products.Select(x => new
        {
            x.ProductId,
            x.Title,
            x.CategoryName,
            x.City,
            x.District,
            x.Price
        });
        var productText = JsonConvert.SerializeObject(shortList);

        var systemPrompt =
            "Sen bir emlak danışmanısın. Kullanıcının isteklerine göre SADECE aşağıdaki ilan listesinden öneri yap. " +
            "Listede olmayan ilan uydurma. Uygun ilan yoksa açıkça söyle ve kriterleri gevşetmeyi öner. " +
            "Her öneri için ilan başlığını, şehrini, fiyatını ve neden uygun olduğunu kısaca yaz. Türkçe cevap ver.\n\n" +
            "İLAN LİSTESİ:\n" + productText;


        var aiClient = _httpClientFactory.CreateClient();
        aiClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _configuration["OpenAI:ApiKey"]);

        var requestData = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = message }
            },
            temperature = 0.3
        };
        var jsonData= JsonConvert.SerializeObject(requestData);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await aiClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            using var document = JsonDocument.Parse(responseString);
            var answer = document.RootElement
                                 .GetProperty("choices")[0]
                                 .GetProperty("message")
                                 .GetProperty("content")
                                 .GetString();

            ViewBag.Answer = answer;
        }
        else
        {
            ViewBag.Answer = "Asistan şu an cevap veremiyor: " + response.StatusCode;
        }

        return View();
    }
}