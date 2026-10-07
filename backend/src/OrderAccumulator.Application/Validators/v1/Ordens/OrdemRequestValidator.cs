using FluentValidation;
using OrderAccumulator.Application.DTOs.v1.Ordens;
using OrderAccumulator.Domain.Enums;

namespace OrderAccumulator.Application.Validators.v1.Ordens;

public sealed class OrdemRequestValidator : AbstractValidator<OrdemRequest>
{
    public OrdemRequestValidator()
    {
        RuleFor(x => x.Ativo)
            .NotEmpty().WithMessage("O ativo é obrigatório.")
            .Must(BeAValidAtivo).WithMessage("O ativo informado não é válido.");

        RuleFor(x => x.Lado)
            .NotEmpty().WithMessage("O lado da ordem é obrigatório.")
            .Must(BeAValidLado).WithMessage("O lado da ordem informado não é válido.");

        RuleFor(x => x.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.")
            .LessThan(100000).WithMessage("A quantidade deve ser menor que 100000.");

        RuleFor(x => x.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.")
            .LessThan(1000).WithMessage("O preço deve ser menor que 1.000.");
    }

    private bool BeAValidAtivo(string ativo)
    {
        return Enum.TryParse(typeof(Ativo), ativo, true, out _);
    }

    private bool BeAValidLado(string lado)
    {
        return Enum.TryParse(typeof(Lado), lado, true, out _);
    }
}