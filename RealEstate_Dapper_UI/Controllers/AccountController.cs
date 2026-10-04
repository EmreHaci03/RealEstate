using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.DTOS.LoginDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealEstate_Dapper_UI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ResultLoginDto dto)
        {
            // 1. API'ye kullanıcı adı ve şifreyi gönder
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7025/api/Login", content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
                return View(dto);
            }

            // 2. Gelen token'ı oku
            var tokenJsonData = await responseMessage.Content.ReadAsStringAsync();
            var tokenModel = JsonConvert.DeserializeObject<TokenModel>(tokenJsonData)!;

            // 3. Token'ın içindeki claim'leri çıkar
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenModel.Token);
            var claims = jwt.Claims.ToList();

            // 4. Token'ın kendisini de sakla, API'ye istek atarken lazım olacak
            claims.Add(new Claim("AccessToken", tokenModel.Token));

            // 5. Çerezle giriş yap
            var identity = new ClaimsIdentity(
    claims,                                  // 1. Claim listesi
    JwtBearerDefaults.AuthenticationScheme,  // 2. Hangi yöntemle giriş yapıldı
    "Username",                              // 3. Kullanıcı adı hangi claim'de?
    ClaimTypes.Role);                                 // 4. Rol hangi claim'de?

            await HttpContext.SignInAsync(
                JwtBearerDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    ExpiresUtc = tokenModel.ExpireDate,   // çerez, token'la birlikte biter
                    IsPersistent = true
                });

            // 6. Rolüne göre yönlendir
            var role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if (role == "Admin")
                return Redirect("/Admin/Dashboard/Index");

            if (role == "Member")
                return Redirect("/EstateAgent/Adverts/Index");

            return Redirect("/");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}