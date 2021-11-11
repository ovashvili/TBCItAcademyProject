using Events.ItAcademy.Ge.AdminMVC.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.AdminMVC.Validators
{
    public class PublisherViewModelValidator : AbstractValidator<PublisherViewModel>
    {
        public PublisherViewModelValidator()
        {
            RuleFor(x => x.PublisherID)
                .NotEmpty();

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Length(0, 80);
        }
    }
}
