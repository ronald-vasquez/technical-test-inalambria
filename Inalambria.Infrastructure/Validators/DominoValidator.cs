using FluentValidation;
using Inalambria.Core.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Infrastructure.Validators
{
    public class DominoValidator : AbstractValidator<DominoRequest>
    {
        public DominoValidator()
        {
            RuleFor(x => x.Dominos)
                .NotNull()
                .WithMessage("Domino is required.")
                .Must(x => x.Count >= 2 && x.Count <= 6)
                .WithMessage("The tile set must have between 2 and 6 tiles");

            RuleForEach(x => x.Dominos).ChildRules(y =>
            {
                y.RuleFor(z => z.Start)
                .NotNull()
                .WithMessage("The number is required")
                .Must(z => z >= 0)
                .WithMessage("The start number must be greater than or equal to 0");
            });

            RuleForEach(x => x.Dominos).ChildRules(y =>
            {
                y.RuleFor(z => z.End)
                .NotNull()
                .WithMessage("The number is required")
                .Must(z => z <= 6)
                .WithMessage("The start end must be greater than or equal to 6");
            });
        }
    }
}
