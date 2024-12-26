using FluentValidation;
using TaskManagement.Features.Tasks.Requests;

namespace TaskManagement.Features.Tasks.Validations
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskValidator()
        {
            //RuleForEach();

            RuleFor(task => task.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");

            RuleFor(task => task.Title)
                .NotNull().WithMessage("Title is required.")
                .NotEmpty().WithMessage("Title cannot be empty.");

            RuleFor(task => task.Status)
                .IsInEnum().WithMessage("Invalid status.");

            RuleFor(task => task.Priority)
                .IsInEnum().WithMessage("Invalid priority.");

            RuleFor(task => task.Tags)
                .NotNull().WithMessage("Tags cannot be null.");

            RuleForEach(task => task.Tags)
                .ChildRules(tag =>
                {
                    tag.RuleFor(t => t.Name)
                       .NotNull().NotEmpty().WithMessage("Tag name is required.")
                       .MaximumLength(50).WithMessage("Tag name cannot exceed 50 characters.");
                });
        }
    }
}
