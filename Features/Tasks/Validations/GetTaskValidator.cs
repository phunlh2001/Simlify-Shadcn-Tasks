using FluentValidation;
using TaskManagement.Features.Tasks.Requests;

namespace TaskManagement.Features.Tasks.Validations
{
    public class GetTaskValidator : AbstractValidator<GetTasksRequest>
    {
        public GetTaskValidator()
        {
            RuleFor(p => p.SortOrder)
                .NotEmpty().WithMessage("SortOrder is required!")
                .Must(value => string.IsNullOrEmpty(value) || value.ToUpper() == "ASC" || value.ToUpper() == "DESC")
                .WithMessage("SortOrder only accepts 'ASC' or 'DESC' value");

            RuleFor(p => p.SortBy)
                .NotEmpty().WithMessage("SortBy cannot be null or empty!");
        }
    }
}
