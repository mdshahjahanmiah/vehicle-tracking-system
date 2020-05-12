using System;
using VehicleTrackingSystem.DataObjects.ApiSettings;
using VehicleTrackingSystem.Security.Handlers;
using Xunit;

namespace VehicleTrackingSystem.UnitTest
{
    public class SecurityLibraryTest
    {
        
        [Fact]
        public void GeneratePasswordHashSuccessMethod()
        {
            var password = "123456";
            var _cryptographyHandler = new CryptographyHandler();
            var hashPassword = _cryptographyHandler.GeneratePasswordHash(password);
            Assert.True(!string.IsNullOrEmpty(hashPassword));
        }
        [Fact]
        public void VerifyGeneratedHashSuccessMethod()
        {
            var password = "123456";
            var savedPasswordHash = "9SX59yDbWpfRpbGfTqNnqw2y8AA6E+TEvu5aWCx3fl+bRblA";
            var _cryptographyHandler = new CryptographyHandler();
            Assert.True(_cryptographyHandler.VerifyGeneratedHash(password, savedPasswordHash));
        }
        [Fact]
        public void GenerateJwtSecurityTokenSuccessMethod()
        {
            AppSettings appSettings = new AppSettings();
            appSettings.Secret = "1234567890 a very long word";
            string userId = "1";
            var token = new JwtTokenHandler(appSettings).GenerateJwtSecurityToken(userId);
            Assert.True(!string.IsNullOrEmpty(token));
        }
        [Fact]
        public void VerifyJwtSecurityTokenSuccessMethod()
        {
            AppSettings appSettings = new AppSettings();
            appSettings.Secret = "1234567890 a very long word";
            var _jwtTokenHandler = new JwtTokenHandler(appSettings);
            var user = _jwtTokenHandler.VerifyJwtSecurityToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1Njk5NTUwOTgsImV4cCI6MTU3MDU1OTg5OCwiaWF0IjoxNTY5OTU1MDk4fQ._d2vCroRoYMGfB76AG14gorMaVcowiOpp6mf_s49zuE");
            Assert.Equal("1",user);
        }
    }
}
