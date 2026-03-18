using MediatR;

namespace PythonOgrenme.Application.Commands.Questions;

public class DeleteQuestionCommand : IRequest<Unit>
{
    public int SoruId { get; set; }
}