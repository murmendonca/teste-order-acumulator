using FluentValidation;
using OrderAccumulator.Application.DTOs.v1;
using OrderAccumulator.Application.DTOs.v1.Ordens;
using OrderAccumulator.Application.Interfaces.v1;
using OrderAccumulator.Application.Interfaces.v1.Repositories;
using OrderAccumulator.Application.Interfaces.v1.Services;
using OrderAccumulator.Domain.Entities.v1;
using OrderAccumulator.Domain.Enums;
using OrderAccumulator.Domain.Shared.v1;

namespace OrderAccumulator.Application.Services.v1;

public class OrdemService(
    IValidator<OrdemRequest> validator,
    IOrdemRepository ordemRepository
) : IOrdemService
{
    public async Task<Resultado> ProcessarOrdemAsync(OrdemRequest ordemRequest)
    {
        var validacao = await ValidarAsync(ordemRequest);
        if (!validacao.Sucesso) return validacao;
        
        var ordem = new Ordem(
            Enum.Parse<Lado>(ordemRequest.Lado!),
            Enum.Parse<Ativo>(ordemRequest.Ativo!),
            ordemRequest.Quantidade,
            ordemRequest.Preco
        );
        
        var exposicaoAtual = await ordemRepository.GetExposicaoPorAtivoAsync(ordem.Ativo);
        var novaExposicao = CalculadoraExposicao.CalcularNovaExposicao(exposicaoAtual, ordem);
        
        if (CalculadoraExposicao.UltrapassaLimite(novaExposicao))
        {
            return new Resultado().Erro(exposicaoAtual, "Limite de exposição por ativo ultrapassado.");
        }
        
        await ordemRepository.AddOrdemAsync(ordem);

        return new Resultado().Ok(novaExposicao);
    }
    
    private async Task<Resultado> ValidarAsync(OrdemRequest request)
    {
        var validacao = await validator.ValidateAsync(request);
        if (validacao.IsValid) return new Resultado().Ok(0);
        
        var erros = string.Join("; ", validacao.Errors.Select(e => e.ErrorMessage));
        return new Resultado().Erro(0, erros);
    }
}