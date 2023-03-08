using Inalambria.Core.DTOs;
using Inalambria.Core.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Core.Interfaces.Services
{
    public interface IDominoService
    {
        List<DominoDtos> BuildDomino(DominoRequest dominoList);
    }
}
