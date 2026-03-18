using MediatR;
using PythonOgrenme.Application.DTOs;

namespace PythonOgrenme.Application.Commands.Answers;

public class SubmitAnswerCommand : IRequest<SubmitAnswerResult>
{
    public int KullaniciId { get; set; }
    public int SoruId { get; set; }
    public string VerilenCevap { get; set; } = string.Empty;
}