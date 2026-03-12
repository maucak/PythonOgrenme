namespace PythonOgrenme.Domain.Entities;

public class Answer : BaseEntity
{
    public int KullaniciId { get; private set; }
    public int SoruId { get; private set; }
    public string VerilenCevap { get; private set; } = string.Empty;
    public bool DogruMu { get; private set; }
    public DateTime CevapTarihi { get; private set; } = DateTime.UtcNow;
    public int DenemeNo { get; private set; } = 1;

    // Navigation properties
    public User? User { get; private set; }
    public Question? Question { get; private set; }

    protected Answer() { }

    public Answer(int kullaniciId, int soruId, string verilenCevap, bool dogruMu, int denemeNo = 1)
    {
        KullaniciId = kullaniciId;
        SoruId = soruId;
        VerilenCevap = verilenCevap;
        DogruMu = dogruMu;
        DenemeNo = denemeNo;
        CevapTarihi = DateTime.UtcNow;
    }
}