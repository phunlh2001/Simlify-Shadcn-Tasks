using FluentValidation;
using TaskManagement.Features.Tags.Models;

namespace TaskManagement.Features.Tags.Validations
{
    public class GetTagsValidator : AbstractValidator<GetTagsRequest>
    {
        public GetTagsValidator()
        {
            RuleFor(x => x.Total).NotNull().NotEmpty().WithMessage("Missing 'total' field");

            RuleFor(x => x.Page).NotNull().NotEmpty().WithMessage("Missing 'skip' field");
        }
    }
}
