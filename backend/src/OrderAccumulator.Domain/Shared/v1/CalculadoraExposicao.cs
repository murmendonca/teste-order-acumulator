using OrderAccumulator.Domain.Entities.v1;
using OrderAccumulator.Domain.Shared.v1.Constants;

namespace OrderAccumulator.Domain.Shared.v1;

public static class CalculadoraExposicao
{
    public static decimal CalcularNovaExposicao(decimal exposicaoAtual, Ordem ordem)
        => exposicaoAtual + ordem.ValorExposicao;
    
    public static bool UltrapassaLimite(decimal exposicao)
        => Math.Abs(exposicao) > LimitesRisco.LimiteExposicaoPorAtivo;
}