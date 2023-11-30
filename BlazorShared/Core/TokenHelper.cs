using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Core
{
    public static class TokenHelper
    {
        public static bool IsTokenExpired(string token)
        {
            List<Claim> claims = ParseClaimsFromJwt(token).ToList();
            if (claims?.Count == 0)
                return true;

            string expirationSeconds = claims.Where(_ => _.Type.ToLower() == "exp").Select(_ => _.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(expirationSeconds))
                return true;

            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expirationSeconds));
            if (expirationDate < DateTime.UtcNow)
                return true;

            return false;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt);
            var token = jsonToken as JwtSecurityToken;

            foreach (var claim in token.Claims)
            {
                claims.Add(claim);
            }


            return claims;
        }

    }
}
