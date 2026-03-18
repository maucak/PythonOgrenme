using MediatR;
using PythonOgrenme.Application.DTOs;

namespace PythonOgrenme.Application.Commands.Auth;

public class LoginCommand : IRequest<LoginResponseDto>
{
    public string KullaniciAdi { get; set; } = string.Empty;
    public string Sifre { get; set; } = string.Empty;
}