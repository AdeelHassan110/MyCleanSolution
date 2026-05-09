using MediatR;
using Application.Interfaces;
using Application.DTOs;

namespace Application.Queries.GetStudentById;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
{
    private readonly IStudentRepository _studentRepo;

    public GetStudentByIdQueryHandler(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepo.GetByIdAsync(request.Id);

        if (student == null) return null;

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age
        };
    }
}