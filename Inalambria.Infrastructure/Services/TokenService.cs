using Inalambria.Core.Interfaces.Infraestructure;
using Inalambria.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly AuthenticationOptions _options;
        private readonly ILoggerService _logger;

        public TokenService(IOptions<AuthenticationOptions> options, ILoggerService logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public string GenerateToken(string Email)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[] {
                new Claim(ClaimTypes.Email, Email)
            };

            //Payload
            var payload = new JwtPayload(
                    _options.Issuer,
                    _options.Audience,
                    claims,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddMinutes(_options.ExpireMinutesToken)
                );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<Claim> GetTokenClaims(string jwt)
        {
            try
            {
                JwtSecurityToken token = new JwtSecurityToken(jwtEncodedString: jwt);
                return token?.Claims.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in TokenService->DecodeToken-> { Exception}", ex.Message);
                return new List<Claim>();
            }
        }
    }
}
