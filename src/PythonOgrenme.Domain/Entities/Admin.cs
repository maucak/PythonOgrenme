namespace PythonOgrenme.Domain.Entities;

public class Admin : BaseEntity
{
    public string KullaniciAdi { get; private set; } = string.Empty;
    public string SifreHash { get; private set; } = string.Empty;
    public string? Email { get; private set; }

    protected Admin() { }

    public Admin(string kullaniciAdi, string sifreHash, string? email = null)
    {
        KullaniciAdi = kullaniciAdi;
        SifreHash = sifreHash;
        Email = email;
    }
}