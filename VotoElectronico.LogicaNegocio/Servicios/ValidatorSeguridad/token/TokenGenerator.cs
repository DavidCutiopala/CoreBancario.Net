using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Security.Claims;

namespace VotoElectronico.Api.Controllers
{
    internal static class TokenGenerator
    {
        public static string GenerateTokenJwt(string username, string hora)
        {

            // appsetting for Token JWT
            var secretKey = ConfigurationManager.AppSettings["ClaveSecreta"];
            var audienceToken = ConfigurationManager.AppSettings["Audience"];
            var issuerToken = ConfigurationManager.AppSettings["Issuer"];
            if (!Int32.TryParse(ConfigurationManager.AppSettings["Expires"], out int expireTime))
                expireTime = 90;

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username, hora) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}