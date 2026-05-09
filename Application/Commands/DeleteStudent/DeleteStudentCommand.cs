using MediatR;

namespace Application.Commands.DeleteStudent;

public class DeleteStudentCommand : IRequest<bool>
{
    public int Id { get; set; }
}