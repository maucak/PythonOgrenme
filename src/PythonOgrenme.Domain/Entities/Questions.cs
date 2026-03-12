using PythonOgrenme.Domain.Enums;

namespace PythonOgrenme.Domain.Entities;

public class Question : BaseEntity
{
    public ModulTuru ModulTuru { get; private set; }
    public string SoruMetni { get; private set; } = string.Empty;
    public string KodBlogu { get; private set; } = string.Empty;
    public string DogruCevap { get; private set; } = string.Empty;
    public string? GeriBildirimAciklamasi { get; private set; }
    public ZorluKSeviyesi ZorluKSeviyesi { get; private set; }
    public string? HataTuru { get; private set; }
    public bool AktifMi { get; private set; } = true;

    // Navigation property
    public ICollection<Answer> Answers { get; private set; } = new List<Answer>();

    protected Question() { }

    public Question(
        ModulTuru modulTuru,
        string soruMetni,
        string kodBlogu,
        string dogruCevap,
        ZorluKSeviyesi zorluKSeviyesi,
        string? geriBildirim = null,
        string? hataTuru = null)
    {
        ModulTuru = modulTuru;
        SoruMetni = soruMetni;
        KodBlogu = kodBlogu;
        DogruCevap = dogruCevap;
        ZorluKSeviyesi = zorluKSeviyesi;
        GeriBildirimAciklamasi = geriBildirim;
        HataTuru = hataTuru;
    }

    public bool CevapDogrula(string verilenCevap)
    {
        return DogruCevap.Trim().Equals(
            verilenCevap.Trim(),
            StringComparison.OrdinalIgnoreCase);
    }

    public void Guncelle(
        string soruMetni,
        string kodBlogu,
        string dogruCevap,
        ZorluKSeviyesi zorluKSeviyesi,
        string? geriBildirim,
        string? hataTuru)
    {
        SoruMetni = soruMetni;
        KodBlogu = kodBlogu;
        DogruCevap = dogruCevap;
        ZorluKSeviyesi = zorluKSeviyesi;
        GeriBildirimAciklamasi = geriBildirim;
        HataTuru = hataTuru;
    }

    public void SoftDelete() => AktifMi = false;
    public void Aktifle() => AktifMi = true;
}