using FluentValidation;
using PythonOgrenme.Application.Commands.Answers;

namespace PythonOgrenme.Application.Validators;

public class SubmitAnswerCommandValidator
    : AbstractValidator<SubmitAnswerCommand>
{
    public SubmitAnswerCommandValidator()
    {
        RuleFor(x => x.KullaniciId)
            .GreaterThan(0).WithMessage("Geçersiz kullanıcı.");

        RuleFor(x => x.SoruId)
            .GreaterThan(0).WithMessage("Geçersiz soru.");

        RuleFor(x => x.VerilenCevap)
            .NotEmpty().WithMessage("Cevap boş olamaz.");
    }
}