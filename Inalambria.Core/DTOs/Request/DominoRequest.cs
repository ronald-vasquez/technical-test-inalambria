using Inalambria.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Core.DTOs.Request
{
    public class DominoRequest
    {
        [ModelBinder(BinderType = typeof(TypeBinder<List<DominoDtos>>))]
        public List<DominoDtos> Dominos { get; set; }
    }
}
