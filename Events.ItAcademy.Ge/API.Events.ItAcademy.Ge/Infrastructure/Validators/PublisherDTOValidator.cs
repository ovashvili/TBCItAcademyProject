using API.Events.ItAcademy.Ge.Infrastructure.Extensions;
using API.Events.ItAcademy.Ge.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Infrastructure.Validators
{
    public class PublisherDTOValidator : AbstractValidator<PublisherDTO>
    {
        public PublisherDTOValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Length(0, 80);
        }
    }
}
