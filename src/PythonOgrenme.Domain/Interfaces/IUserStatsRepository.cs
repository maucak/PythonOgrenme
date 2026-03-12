using PythonOgrenme.Domain.Entities;

namespace PythonOgrenme.Domain.Interfaces;

public interface IUserStatsRepository
{
    Task<UserStats?> GetByKullaniciIdAsync(int kullaniciId);
    Task AddAsync(UserStats stats);
    Task UpdateAsync(UserStats stats);
    Task SaveChangesAsync();
}