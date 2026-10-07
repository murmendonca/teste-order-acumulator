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
            .Must(p => p % 0.01m == 0).WithMessage("O preço deve ser múltiplo de 0,01.")
            .LessThan(1000).WithMessage("O preço deve ser menor que 1.000.");
    }

    private bool BeAValidAtivo(string ativo) => Enum.GetNames<Ativo>().Contains(ativo);
    private bool BeAValidLado(string lado) => Enum.GetNames<Lado>().Contains(lado);
}