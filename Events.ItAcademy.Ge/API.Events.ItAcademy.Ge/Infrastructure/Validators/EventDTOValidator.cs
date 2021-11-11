using API.Events.ItAcademy.Ge.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Infrastructure.Validators
{
    public class EventDTOValidator : AbstractValidator<EventDTO>
    {
        public EventDTOValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Length(0, 80);

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Length(0, 40);

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Length(0, 100);

            RuleFor(x => x.Date)
                .GreaterThan(x => DateTime.Now);

            RuleFor(x => x.PublisherID)
                .NotEmpty();
        }
    }
}
