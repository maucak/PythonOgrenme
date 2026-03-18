using MediatR;
using PythonOgrenme.Domain.Enums;

namespace PythonOgrenme.Application.Commands.Questions;

public class CreateQuestionCommand : IRequest<int>
{
    public ModulTuru ModulTuru { get; set; }
    public string SoruMetni { get; set; } = string.Empty;
    public string KodBlogu { get; set; } = string.Empty;
    public string DogruCevap { get; set; } = string.Empty;
    public ZorluKSeviyesi ZorluKSeviyesi { get; set; }
    public string? GeriBildirimAciklamasi { get; set; }
    public string? HataTuru { get; set; }
}