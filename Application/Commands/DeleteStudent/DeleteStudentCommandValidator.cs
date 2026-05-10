using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DeleteStudent
{
    public class DeleteStudentCommandValidator:AbstractValidator<DeleteStudentCommand>

    {

        public DeleteStudentCommandValidator()
        {
            RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("Student ID must be greater than 0");
        }
    }
}
