using MediatR;
using PythonOgrenme.Application.DTOs;
using PythonOgrenme.Application.Queries.Questions;
using PythonOgrenme.Domain.Interfaces;

namespace PythonOgrenme.Application.Handlers.Questions;

public class GetQuestionByIdQueryHandler
    : IRequestHandler<GetQuestionByIdQuery, QuestionDto?>
{
    private readonly IQuestionRepository _repo;

    public GetQuestionByIdQueryHandler(IQuestionRepository repo) => _repo = repo;

    public async Task<QuestionDto?> Handle(
        GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repo.GetByIdAsync(request.SoruId);
        if (s == null || !s.AktifMi) return null;

        return new QuestionDto
        {
            SoruId = s.Id,
            ModulTuru = s.ModulTuru,
            SoruMetni = s.SoruMetni,
            KodBlogu = s.KodBlogu,
            ZorluKSeviyesi = s.ZorluKSeviyesi,
            HataTuru = s.HataTuru
        };
    }
}