namespace PythonOgrenme.Domain.Events;

public class QuestionAddedEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public string EventTipi => "QuestionAddedEvent";

    public int SoruId { get; }
    public string ModulTuru { get; }

    public QuestionAddedEvent(int soruId, string modulTuru)
    {
        SoruId = soruId;
        ModulTuru = modulTuru;
    }
}