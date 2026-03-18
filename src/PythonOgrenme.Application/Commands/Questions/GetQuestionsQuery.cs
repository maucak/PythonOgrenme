using MediatR;
using PythonOgrenme.Application.DTOs;
using PythonOgrenme.Domain.Enums;

namespace PythonOgrenme.Application.Queries.Questions;

public class GetQuestionsQuery : IRequest<List<QuestionDto>>
{
    public ModulTuru ModulTuru { get; set; }
    public ZorluKSeviyesi? ZorluKSeviyesi { get; set; } // null = hepsi
    public int KullaniciId { get; set; } // Çözülenleri filtrelemek için
}