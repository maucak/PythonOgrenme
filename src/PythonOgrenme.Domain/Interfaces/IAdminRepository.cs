using PythonOgrenme.Domain.Entities;

namespace PythonOgrenme.Domain.Interfaces;

public interface IAdminRepository
{
    Task<Admin?> GetByKullaniciAdiAsync(string kullaniciAdi);
    Task AddAsync(Admin admin);
    Task SaveChangesAsync();
}