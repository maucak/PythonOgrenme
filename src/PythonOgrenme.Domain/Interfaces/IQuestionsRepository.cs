using PythonOgrenme.Domain.Entities;
using PythonOgrenme.Domain.Enums;

namespace PythonOgrenme.Domain.Interfaces;

public interface IQuestionRepository
{
    Task<Question?> GetByIdAsync(int id);
    Task<IEnumerable<Question>> GetAllActiveAsync();
    Task<IEnumerable<Question>> GetByModulAsync(ModulTuru modulTuru);
    Task<int> GetTotalActiveCountAsync();
    Task AddAsync(Question question);
    Task UpdateAsync(Question question);
    Task SaveChangesAsync();
}