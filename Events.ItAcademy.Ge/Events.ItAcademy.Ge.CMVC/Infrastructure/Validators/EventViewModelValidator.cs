using Events.ItAcademy.Ge.CMVC.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.CMVC.Infrastructure.Validators
{
    public class EventViewModelValidator : AbstractValidator<EventViewModel>
    {
        public EventViewModelValidator()
        {
            RuleFor(x => x.EventID)
                .NotEmpty();

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
