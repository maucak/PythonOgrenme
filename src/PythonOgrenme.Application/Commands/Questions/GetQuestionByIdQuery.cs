using MediatR;
using PythonOgrenme.Application.DTOs;

namespace PythonOgrenme.Application.Queries.Questions;

public class GetQuestionByIdQuery : IRequest<QuestionDto?>
{
    public int SoruId { get; set; }
}