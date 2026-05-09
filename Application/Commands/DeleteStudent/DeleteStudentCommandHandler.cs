using MediatR;
using Application.Interfaces;

namespace Application.Commands.DeleteStudent;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IStudentRepository _studentRepo;

    public DeleteStudentCommandHandler(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _studentRepo.GetByIdAsync(request.Id);
        if (existing == null) return false;

        await _studentRepo.DeleteAsync(request.Id);
        return true;
    }
}