using MediatR;
using PythonOgrenme.Application.Commands.Questions;
using PythonOgrenme.Domain.Interfaces;

namespace PythonOgrenme.Application.Handlers.Questions;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Unit>
{
    private readonly IQuestionRepository _repo;

    public DeleteQuestionCommandHandler(IQuestionRepository repo) => _repo = repo;

    public async Task<Unit> Handle(
        DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var soru = await _repo.GetByIdAsync(request.SoruId)
            ?? throw new KeyNotFoundException($"Soru bulunamadı: {request.SoruId}");

        soru.SoftDelete(); // AktifMi = false, DB'den silinmez
        await _repo.UpdateAsync(soru);
        await _repo.SaveChangesAsync();
        return Unit.Value;
    }
}