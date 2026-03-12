namespace PythonOgrenme.Domain.Entities;

public class UserStats : BaseEntity
{
    public int KullaniciId { get; private set; }
    public int ToplamPuan { get; private set; } = 0;
    public int TamamlananSoruSayisi { get; private set; } = 0;
    public int StreakSayisi { get; private set; } = 0;
    public float IlerlemeYuzdesi { get; private set; } = 0f;
    public DateTime SonGuncellemeTarihi { get; private set; } = DateTime.UtcNow;

    // Navigation property
    public User? User { get; private set; }

    protected UserStats() { }

    public UserStats(int kullaniciId)
    {
        KullaniciId = kullaniciId;
    }

    public void Guncelle(int toplamPuan, int tamamlananSoru, int streak, int toplamAktifSoru)
    {
        ToplamPuan = toplamPuan;
        TamamlananSoruSayisi = tamamlananSoru;
        StreakSayisi = streak;
        IlerlemeYuzdesi = toplamAktifSoru > 0
            ? (float)tamamlananSoru / toplamAktifSoru * 100f
            : 0f;
        SonGuncellemeTarihi = DateTime.UtcNow;
    }
}