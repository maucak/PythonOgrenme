using PythonOgrenme.Domain.Entities;

namespace PythonOgrenme.Domain.Interfaces;

public interface IAnswerRepository
{
    Task<IEnumerable<Answer>> GetByKullaniciIdAsync(int kullaniciId);
    Task<Answer?> GetByKullaniciVeSoruAsync(int kullaniciId, int soruId);
    Task<int> GetDenemeNoAsync(int kullaniciId, int soruId);
    Task<IEnumerable<int>> GetTamamlananSoruIdleriAsync(int kullaniciId);
    Task AddAsync(Answer answer);
    Task SaveChangesAsync();
}