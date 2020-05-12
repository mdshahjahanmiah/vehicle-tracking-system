using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleTrackingSystem.DataObjects.ApiSettings;

namespace VehicleTrackingSystem.Security.Handlers
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly AppSettings _appSettings;
        public JwtTokenHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public string GenerateJwtSecurityToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }
        public string VerifyJwtSecurityToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signinKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var user = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken).Identity.Name;
                return user;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            
        }
    }
}
