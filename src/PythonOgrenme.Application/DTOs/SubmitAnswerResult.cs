namespace PythonOgrenme.Application.DTOs;

public class SubmitAnswerResult
{
    public bool DogruMu { get; set; }
    public string GeriBildirim { get; set; } = string.Empty;
    public int KazanilanPuan { get; set; }
    public int DenemeNo { get; set; }
    public bool SonDeneme { get; set; } // 3. denemeyse true, cevap açıklanır
}