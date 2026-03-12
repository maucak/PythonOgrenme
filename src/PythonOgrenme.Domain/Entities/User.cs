namespace PythonOgrenme.Domain.Entities;

public class User : BaseEntity
{
    public string KullaniciAdi { get; private set; } = string.Empty;
    public string SifreHash { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public int Puan { get; private set; } = 0;
    public string Seviye { get; private set; } = "Acemi";
    public int StreakSayisi { get; private set; } = 0;
    public DateTime? SonGirisTarihi { get; private set; }
    public bool AktifMi { get; private set; } = true;
    public int BasarisizGirisSayisi { get; private set; } = 0;

    // EF Core için parametresiz constructor
    protected User() { }

    public User(string kullaniciAdi, string sifreHash, string? email = null)
    {
        KullaniciAdi = kullaniciAdi;
        SifreHash = sifreHash;
        Email = email;
    }

    public void PuanEkle(int miktar)
    {
        if (miktar <= 0) return;
        Puan += miktar;
        SeviyeGuncelle();
    }

    private void SeviyeGuncelle()
    {
        Seviye = Puan switch
        {
            < 100 => "Acemi",
            < 250 => "Çırak",
            < 500 => "Deneyimli",
            < 1000 => "Usta",
            _ => "Python Hâkimi"
        };
    }

    public void StreakGuncelle()
    {
        var bugun = DateTime.UtcNow.Date;

        if (SonGirisTarihi == null)
        {
            StreakSayisi = 1;
        }
        else
        {
            var fark = (bugun - SonGirisTarihi.Value.Date).Days;

            if (fark == 0) return;        // bugün zaten giriş yapıldı
            else if (fark == 1) StreakSayisi++;   // seri devam
            else StreakSayisi = 1;        // seri kırıldı, sıfırla
        }

        SonGirisTarihi = DateTime.UtcNow;
    }

    public void BasarisizGirisEkle()
    {
        BasarisizGirisSayisi++;
        if (BasarisizGirisSayisi >= 3)
            AktifMi = false;
    }

    public void BasarisizGirisSifirla()
    {
        BasarisizGirisSayisi = 0;
    }
}