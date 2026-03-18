using MediatR;
using PythonOgrenme.Application.Commands.Questions;
using PythonOgrenme.Domain.Entities;
using PythonOgrenme.Domain.Events;
using PythonOgrenme.Domain.Interfaces;

namespace PythonOgrenme.Application.Handlers.Questions;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, int>
{
    private readonly IQuestionRepository _repo;
    private readonly IRabbitMQPublisher _publisher;

    public CreateQuestionCommandHandler(
        IQuestionRepository repo, IRabbitMQPublisher publisher)
    {
        _repo = repo;
        _publisher = publisher;
    }

    public async Task<int> Handle(
        CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var soru = new Question(
            request.ModulTuru,
            request.SoruMetni,
            request.KodBlogu,
            request.DogruCevap,
            request.ZorluKSeviyesi,
            request.GeriBildirimAciklamasi,
            request.HataTuru);

        await _repo.AddAsync(soru);
        await _repo.SaveChangesAsync();

        await _publisher.PublishAsync(new QuestionAddedEvent(
            soru.Id, request.ModulTuru.ToString()));

        return soru.Id;
    }
}