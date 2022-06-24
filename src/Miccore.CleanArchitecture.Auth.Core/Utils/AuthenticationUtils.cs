using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Miccore.CleanArchitecture.Auth.Core.Utils
{
    /// <summary>
    /// auth utils class for all value of dates
    /// </summary>
    public static class AuthenticationUtils
    {
        /// <summary>
        /// generate refresh token
        /// </summary>
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// generate token
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
         public static string GenerateToken(Miccore.CleanArchitecture.Auth.Core.Entities.User User)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("322e9998-f1f0-494a-9b9d-aea4e0008888"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var guid = Guid.NewGuid().ToString();
            List<Claim> claims = new List<Claim>(){
                new Claim(JwtRegisteredClaimNames.Sub, User.FirstName),
                new Claim(ClaimTypes.Name, User.FirstName),
                new Claim(ClaimTypes.MobilePhone, User.Phone ?? ""),
                new Claim(ClaimTypes.Email, User.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, guid),
                new Claim(ClaimTypes.Role, User.Role.Name),
            };
            
            var token = new JwtSecurityToken(
                                            issuer: "",
                                            audience: "",
                                            claims: claims,
                                            expires: DateTime.Now.AddMinutes(3600),
                                            signingCredentials: credentials
                                            );
                                            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}