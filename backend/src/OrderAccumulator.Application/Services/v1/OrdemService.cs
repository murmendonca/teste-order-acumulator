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
        var validacao = await validator.ValidateAsync(ordemRequest);
        if (!validacao.IsValid)
        {
            var erros = string.Join("; ", validacao.Errors.Select(e => e.ErrorMessage));
            return new Resultado
            {
                Sucesso = false,
                ExposicaoAtual = 0,
                Mensagem = erros
            };
        }
        
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
            return new Resultado
            {
                Sucesso = false,
                ExposicaoAtual = exposicaoAtual,
                Mensagem = "Limite de exposição por ativo ultrapassado."
            };
        }
        
        await ordemRepository.AddOrdemAsync(ordem);
        
        return new Resultado
        {
            Sucesso = true,
            ExposicaoAtual = novaExposicao,
            Mensagem = "Ordem processada com sucesso."
        };
    }
}