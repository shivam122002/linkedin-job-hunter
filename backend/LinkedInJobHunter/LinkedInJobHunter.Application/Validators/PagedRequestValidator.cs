using FluentValidation;
using LinkedInJobHunter.Application.Models;

namespace LinkedInJobHunter.Application.Validators
{
    public class PagedRequestValidator : AbstractValidator<PagedRequest>
    {
        public PagedRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(50);
        }
    }
}