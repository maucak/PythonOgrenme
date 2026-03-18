using MediatR;
using PythonOgrenme.Application.DTOs;

namespace PythonOgrenme.Application.Queries.Users;

public class GetUserStatsQuery : IRequest<UserStatsDto?>
{
    public int KullaniciId { get; set; }
}