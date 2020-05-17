using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Api.Internal
{
    public class ClaimPrincipal : ClaimsPrincipal
    {
        public string NameIdentifier { get; }
        public ClaimPrincipal(ClaimsPrincipal principal) : base(principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal), "ClaimsPrincipal Identity must not be null");
            if (principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                NameIdentifier = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            else
                NameIdentifier = "Anonymous";      
        }

        public static string GenerateToken(string username)
        {
            var env = Environment.GetEnvironmentVariable("TodoList_Token_Key");
            var signingKey = Encoding.UTF8.GetBytes(env);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, username),
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("Api", "Clients", claims, expires: DateTime.UtcNow.AddMonths(12), signingCredentials: creds);

            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);

        }
   
    }
}
