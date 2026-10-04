using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate_Dapper_Api.Tools
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel viewModel)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrWhiteSpace(viewModel.Role))
                claims.Add(new Claim(ClaimTypes.Role, viewModel.Role));


            if (!string.IsNullOrWhiteSpace(viewModel.UserName))
                claims.Add(new Claim("Username", viewModel.UserName));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, viewModel.Id.ToString()));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpireMinutes"]!));


            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: expireDate,
                signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponseViewModel(tokenString, expireDate);

        }
    }
}
