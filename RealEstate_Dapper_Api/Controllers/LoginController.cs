using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.LoginDtos;
using RealEstate_Dapper_Api.Repository.Login;
using RealEstate_Dapper_Api.Tools;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public LoginController(ILoginRepository loginRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _loginRepository = loginRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(ResultLoginDto dto)
        {
            var values = await _loginRepository.CheckUserAsync(dto);

            if (!values.IsExists)
                return Unauthorized("Kullanıcı adı veya şifre hatalı");

            var token = _jwtTokenGenerator.GenerateToken(values);
            return Ok(token);
        }
    }
}
