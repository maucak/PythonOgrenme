namespace PythonOgrenme.Domain.Events;

public class StreakUpdatedEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public string EventTipi => "StreakUpdatedEvent";

    public int KullaniciId { get; }
    public string KullaniciAdi { get; }
    public int YeniStreak { get; }

    public StreakUpdatedEvent(int kullaniciId, string kullaniciAdi, int yeniStreak)
    {
        KullaniciId = kullaniciId;
        KullaniciAdi = kullaniciAdi;
        YeniStreak = yeniStreak;
    }
}