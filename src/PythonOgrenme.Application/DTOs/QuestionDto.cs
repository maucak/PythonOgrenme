using PythonOgrenme.Domain.Enums;

namespace PythonOgrenme.Application.DTOs;

public class QuestionDto
{
    public int SoruId { get; set; }
    public ModulTuru ModulTuru { get; set; }
    public string SoruMetni { get; set; } = string.Empty;
    public string KodBlogu { get; set; } = string.Empty;
    public ZorluKSeviyesi ZorluKSeviyesi { get; set; }
    public string? HataTuru { get; set; }
    // DogruCevap kasıtlı olarak YOK — istemciye gönderilmez
}