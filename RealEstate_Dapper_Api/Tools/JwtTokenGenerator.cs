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

        public TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel viewModel)
        {
            var claims = new List<Claim>();

            // Veritabanındaki sayıyı enum adına çevir: 2 → "Member"
            // Rolü metne çevir (int de olsa string de olsa çalışır)
            string roleValue = Convert.ToString(viewModel.Role) ?? "";

            string roleName = roleValue switch
            {
                "1" => "Admin",
                "2" => "Member",
                "3" => "Visitor",
                "4" => "Manager",
                "5" => "Agent",
                _ => roleValue
            };

            if (!string.IsNullOrWhiteSpace(roleName))
                claims.Add(new Claim(ClaimTypes.Role, roleName));

            if (!string.IsNullOrWhiteSpace(viewModel.UserName))
                claims.Add(new Claim("Username", viewModel.UserName));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, viewModel.Id.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpireMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: expireDate,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponseViewModel(tokenString, expireDate);
        }
    }
}