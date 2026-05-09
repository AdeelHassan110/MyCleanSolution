using MediatR;
using Application.DTOs;

namespace Application.Queries.GetAllStudents;

public class GetAllStudentsQuery : IRequest<List<StudentDto>>
{
}