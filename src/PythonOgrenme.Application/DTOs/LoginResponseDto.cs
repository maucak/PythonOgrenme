namespace PythonOgrenme.Application.DTOs;

public enum KullaniciRol { Ogrenci, Admin }

public class LoginResponseDto
{
    public int KullaniciId { get; set; }
    public string KullaniciAdi { get; set; } = string.Empty;
    public KullaniciRol Rol { get; set; }
    public string Seviye { get; set; } = string.Empty;
    public int Puan { get; set; }
}