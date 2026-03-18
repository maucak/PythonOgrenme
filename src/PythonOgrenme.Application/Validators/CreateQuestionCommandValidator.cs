using FluentValidation;
using PythonOgrenme.Application.Commands.Questions;

namespace PythonOgrenme.Application.Validators;

public class CreateQuestionCommandValidator
    : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(x => x.SoruMetni)
            .NotEmpty().WithMessage("Soru metni boş olamaz.")
            .MinimumLength(10).WithMessage("Soru metni en az 10 karakter olmalıdır.");

        RuleFor(x => x.KodBlogu)
            .NotEmpty().WithMessage("Kod bloğu boş olamaz.")
            .MinimumLength(5).WithMessage("Kod bloğu en az 5 karakter olmalıdır.");

        RuleFor(x => x.DogruCevap)
            .NotEmpty().WithMessage("Doğru cevap boş olamaz.");

        RuleFor(x => x.ModulTuru)
            .IsInEnum().WithMessage("Geçersiz modül türü.");

        RuleFor(x => x.ZorluKSeviyesi)
            .IsInEnum().WithMessage("Geçersiz zorluk seviyesi.");
    }
}