using FluentValidation;
using TaskManagement.Features.Tasks.Requests;

namespace TaskManagement.Features.Tasks.Validations
{
    public class GetTaskValidator : AbstractValidator<GetTasksRequest>
    {
        public GetTaskValidator()
        {
            RuleFor(p => p.SortOrder)
                .Must(value => string.IsNullOrEmpty(value) || value.ToUpper() == "ASC" || value.ToUpper() == "DESC")
                .WithMessage("SortOrder only accepts 'ASC' or 'DESC' value");
        }
    }
}
