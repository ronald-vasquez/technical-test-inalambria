using Inalambria.Core.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Core.Interfaces.Infraestructure
{
    public interface ILoginService
    {
        Task<string> Login(UserLoginRequest request);
    }
}
