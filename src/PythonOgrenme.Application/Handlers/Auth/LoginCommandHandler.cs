using MediatR;
using PythonOgrenme.Application.Commands.Auth;
using PythonOgrenme.Application.DTOs;
using PythonOgrenme.Domain.Interfaces;

namespace PythonOgrenme.Application.Handlers.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IUserRepository _userRepo;
    private readonly IAdminRepository _adminRepo;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IUserRepository userRepo,
        IAdminRepository adminRepo,
        IPasswordHasher passwordHasher)
    {
        _userRepo = userRepo;
        _adminRepo = adminRepo;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponseDto> Handle(
        LoginCommand request, CancellationToken cancellationToken)
    {
        // Önce Admin tablosunda ara
        var admin = await _adminRepo.GetByKullaniciAdiAsync(request.KullaniciAdi);
        if (admin != null && _passwordHasher.Verify(request.Sifre, admin.SifreHash))
        {
            return new LoginResponseDto
            {
                KullaniciId = admin.Id,
                KullaniciAdi = admin.KullaniciAdi,
                Rol = KullaniciRol.Admin,
                Seviye = "Admin",
                Puan = 0
            };
        }

        // Öğrenci tablosunda ara
        var user = await _userRepo.GetByKullaniciAdiAsync(request.KullaniciAdi);

        if (user == null)
            throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı.");

        if (!user.AktifMi)
            throw new InvalidOperationException(
                "Hesabınız kilitlenmiştir. Lütfen yönetici ile iletişime geçin.");

        if (!_passwordHasher.Verify(request.Sifre, user.SifreHash))
        {
            user.BasarisizGirisEkle();
            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveChangesAsync();
            throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı.");
        }

        // Başarılı giriş
        user.BasarisizGirisSifirla();
        user.StreakGuncelle();
        await _userRepo.UpdateAsync(user);
        await _userRepo.SaveChangesAsync();

        return new LoginResponseDto
        {
            KullaniciId = user.Id,
            KullaniciAdi = user.KullaniciAdi,
            Rol = KullaniciRol.Ogrenci,
            Seviye = user.Seviye,
            Puan = user.Puan
        };
    }
}