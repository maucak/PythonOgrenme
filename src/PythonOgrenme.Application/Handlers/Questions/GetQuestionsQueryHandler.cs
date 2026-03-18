using MediatR;
using PythonOgrenme.Application.DTOs;
using PythonOgrenme.Application.Queries.Questions;
using PythonOgrenme.Domain.Interfaces;

namespace PythonOgrenme.Application.Handlers.Questions;

public class GetQuestionsQueryHandler
    : IRequestHandler<GetQuestionsQuery, List<QuestionDto>>
{
    private readonly IQuestionRepository _questionRepo;
    private readonly IAnswerRepository _answerRepo;

    public GetQuestionsQueryHandler(
        IQuestionRepository questionRepo, IAnswerRepository answerRepo)
    {
        _questionRepo = questionRepo;
        _answerRepo = answerRepo;
    }

    public async Task<List<QuestionDto>> Handle(
        GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        // Kullanıcının zaten doğru çözdüğü soru ID'leri
        var cozulenler = (await _answerRepo
            .GetTamamlananSoruIdleriAsync(request.KullaniciId))
            .ToHashSet();

        var sorular = await _questionRepo.GetByModulAsync(request.ModulTuru);

        return sorular
            .Where(s => !cozulenler.Contains(s.Id))
            .Where(s => request.ZorluKSeviyesi == null
                        || s.ZorluKSeviyesi == request.ZorluKSeviyesi)
            .Select(s => new QuestionDto
            {
                SoruId = s.Id,
                ModulTuru = s.ModulTuru,
                SoruMetni = s.SoruMetni,
                KodBlogu = s.KodBlogu,
                ZorluKSeviyesi = s.ZorluKSeviyesi,
                HataTuru = s.HataTuru
                // DogruCevap burada YOK
            })
            .ToList();
    }
}