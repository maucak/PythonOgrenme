namespace PythonOgrenme.Application.DTOs;

public class UserStatsDto
{
    public string KullaniciAdi { get; set; } = string.Empty;
    public string Seviye { get; set; } = string.Empty;
    public int ToplamPuan { get; set; }
    public int StreakSayisi { get; set; }
    public int TamamlananSoruSayisi { get; set; }
    public float IlerlemeYuzdesi { get; set; }
}