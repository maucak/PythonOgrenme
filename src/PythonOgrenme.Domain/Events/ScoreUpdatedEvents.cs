namespace PythonOgrenme.Domain.Events;

public class ScoreUpdatedEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public string EventTipi => "ScoreUpdatedEvent";

    public int KullaniciId { get; }
    public string KullaniciAdi { get; }
    public int YeniPuan { get; }
    public string YeniSeviye { get; }

    public ScoreUpdatedEvent(int kullaniciId, string kullaniciAdi, int yeniPuan, string yeniSeviye)
    {
        KullaniciId = kullaniciId;
        KullaniciAdi = kullaniciAdi;
        YeniPuan = yeniPuan;
        YeniSeviye = yeniSeviye;
    }
}