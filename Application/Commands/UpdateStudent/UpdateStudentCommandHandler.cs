using MediatR;
using Application.Interfaces;

namespace Application.Commands.UpdateStudent;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, bool>
{
    private readonly IStudentRepository _studentRepo;

    public UpdateStudentCommandHandler(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _studentRepo.GetByIdAsync(request.Id);
        if (existing == null) return false;

        existing.Name = request.Name;
        existing.Email = request.Email;
        existing.Age = request.Age;

        await _studentRepo.UpdateAsync(existing);
        return true;
    }
}