using MediatR;
using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;

namespace Application.Commands.CreateStudent;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
{
    private readonly IStudentRepository _studentRepo;

    public CreateStudentCommandHandler(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age
        };

        await _studentRepo.AddAsync(student);

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age
        };
    }
}