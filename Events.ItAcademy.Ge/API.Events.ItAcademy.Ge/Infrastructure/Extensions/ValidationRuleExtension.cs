using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Infrastructure.Extensions
{
    public static class ValidationRuleExtension
    {
        public static IRuleBuilderOptions<T, string> Phone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => Regex.IsMatch(x, @"^5\d{8}$")).MaximumLength(9);
        }
    }
}
