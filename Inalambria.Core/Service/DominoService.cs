using Inalambria.Core.DTOs;
using Inalambria.Core.DTOs.Request;
using Inalambria.Core.Exceptions;
using Inalambria.Core.Interfaces.Infraestructure;
using Inalambria.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Core.Service
{
    public class DominoService : IDominoService
    {
        private readonly ILoggerService _loggerService;
        public DominoService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public List<DominoDtos> BuildDomino(DominoRequest dominoList)
        {
                _loggerService.LogInformation("Start in DominoService->BuildDomino with {dominoList}",dominoList);
                List<DominoDtos> dominoListNew = CheckDomainList(dominoList);
                _loggerService.LogInformation("End in DominoService->BuildDomino with {dominoListNew}", dominoListNew);
                return dominoListNew;
        }

        private List<DominoDtos> CheckDomainList(DominoRequest dominoList)
        {
            _loggerService.LogInformation("Start in DominoService->CheckDomainList with {dominoList}", dominoList);
            List<DominoDtos> charList = new List<DominoDtos>();
            charList.Add(dominoList.Dominos.First());
            dominoList.Dominos.Remove(dominoList.Dominos.First());
            while (dominoList.Dominos.Any())
            {
                DominoDtos lastDomino = charList.Last();
                DominoDtos dominoNext = dominoList.Dominos.FirstOrDefault(x => x.Start == lastDomino.Start || x.End == lastDomino.End);
                if(dominoNext == null)
                {
                    _loggerService.LogInformation("End in DominoService->CheckDomainList Could not build a valid chain with the given set {domainList}", dominoList);
                    throw new NotFoundException("No se pudo construir una cadena valida con el conjunto dado");
                }
                if(dominoNext.Start == lastDomino.End)
                {
                    charList.Add(dominoNext);
                }
                else
                {
                    charList.Add(new DominoDtos(dominoNext.Start, dominoNext.End));
                }
                dominoList.Dominos.Remove(dominoNext);
            }
            _loggerService.LogInformation("End in DominoService->CheckDomainList end of verification {domainList}", dominoList);
            if(charList.First().Start != charList.Last().End)
            {
                _loggerService.LogInformation("Error in DominoService->CheckDomainList The ends of the string do not match {charList}", charList);
                throw new NotFoundException("Los extremos de la cadena no coinciden");
            }
            _loggerService.LogInformation("End in DominoService->CheckDomainList Result {charList} of {domainList}", charList,dominoList);
            return charList;
        }

        
    }
}
