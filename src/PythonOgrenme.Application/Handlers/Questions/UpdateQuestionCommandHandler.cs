using MediatR;
using PythonOgrenme.Application.Commands.Questions;
using PythonOgrenme.Domain.Interfaces;

namespace PythonOgrenme.Application.Handlers.Questions;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Unit>
{
    private readonly IQuestionRepository _repo;

    public UpdateQuestionCommandHandler(IQuestionRepository repo) => _repo = repo;

    public async Task<Unit> Handle(
        UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var soru = await _repo.GetByIdAsync(request.SoruId)
            ?? throw new KeyNotFoundException($"Soru bulunamadı: {request.SoruId}");

        soru.Guncelle(
            request.SoruMetni, request.KodBlogu, request.DogruCevap,
            request.ZorluKSeviyesi, request.GeriBildirimAciklamasi, request.HataTuru);

        await _repo.UpdateAsync(soru);
        await _repo.SaveChangesAsync();
        return Unit.Value;
    }
}