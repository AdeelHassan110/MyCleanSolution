using FluentValidation;

namespace Application.Commands.CreateStudent;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        // Name validation
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters");

        // Email validation
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters");

        // Age validation
        RuleFor(x => x.Age)
            .NotEmpty().WithMessage("Age is required")
            .InclusiveBetween(5, 100).WithMessage("Age must be between 5 and 100");
    }
}