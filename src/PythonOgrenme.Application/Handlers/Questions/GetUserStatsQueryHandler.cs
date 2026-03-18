using MediatR;
using PythonOgrenme.Application.DTOs;
using PythonOgrenme.Application.Queries.Users;
using PythonOgrenme.Domain.Interfaces;

namespace PythonOgrenme.Application.Handlers.Questions;

public class GetUserStatsQueryHandler
    : IRequestHandler<GetUserStatsQuery, UserStatsDto?>
{
    private readonly IUserStatsRepository _statsRepo;
    private readonly IUserRepository _userRepo;

    public GetUserStatsQueryHandler(
        IUserStatsRepository statsRepo, IUserRepository userRepo)
    {
        _statsRepo = statsRepo;
        _userRepo = userRepo;
    }

    public async Task<UserStatsDto?> Handle(
        GetUserStatsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByIdAsync(request.KullaniciId);
        if (user == null) return null;

        var stats = await _statsRepo.GetByKullaniciIdAsync(request.KullaniciId);

        return new UserStatsDto
        {
            KullaniciAdi = user.KullaniciAdi,
            Seviye = user.Seviye,
            ToplamPuan = user.Puan,
            StreakSayisi = user.StreakSayisi,
            TamamlananSoruSayisi = stats?.TamamlananSoruSayisi ?? 0,
            IlerlemeYuzdesi = stats?.IlerlemeYuzdesi ?? 0f
        };
    }
}