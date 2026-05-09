using MediatR;
using Application.DTOs;

namespace Application.Queries.GetStudentById;

public class GetStudentByIdQuery : IRequest<StudentDto?>
{
    public int Id { get; set; }
}