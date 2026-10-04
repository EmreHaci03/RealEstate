using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Tools;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public TokenController(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost]
        public IActionResult CreateToken(GetCheckAppUserViewModel viewModel)
        {
            var values = _jwtTokenGenerator.GenerateToken(viewModel);
            return Ok(values);
        }
    }
}
