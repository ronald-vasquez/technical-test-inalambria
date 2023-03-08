using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Core.Interfaces.Infraestructure
{
    public interface ITokenService
    {
        string GenerateToken(string Email);
        List<Claim> GetTokenClaims(string jwt);
    }
}
