using OrderAccumulator.Domain.Entities.v1;
using OrderAccumulator.Domain.Enums;
using OrderAccumulator.Domain.Shared.v1;

namespace OrderAccumulator.UnitTests.Domain.v1;

public class CalculadoraExposicaoTests
{
    [Fact]
    public void ORDEM_DE_COMPRA_SOMA_PRECO_VEZES_QUANTIDADE()
    {
        var ordem = new Ordem(Lado.C, Ativo.PETR4, 1, 100m);

        Assert.Equal(100m, CalculadoraExposicao.CalcularNovaExposicao(0m, ordem));
    }

    [Fact]
    public void ORDEM_DE_VENDA_SUBTRAI_PRECO_VEZES_QUANTIDADE()
    {
        var ordem = new Ordem(Lado.V, Ativo.PETR4, 100, 10m);

        Assert.Equal(4000m, CalculadoraExposicao.CalcularNovaExposicao(5000m, ordem));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1000000)]
    [InlineData(-1000000)]
    [InlineData(999999.99)]
    public void EXPOSICAO_DENTRO_DO_LIMITE_NAO_ULTRAPASSA(decimal exposicao)
        => Assert.False(CalculadoraExposicao.UltrapassaLimite(exposicao));

    [Theory]
    [InlineData(1000000.01)]
    [InlineData(-1000000.01)]
    [InlineData(5000000)]
    public void EXPOSICAO_ACIMA_DO_LIMITE_ULTRAPASSA(decimal exposicao)
        => Assert.True(CalculadoraExposicao.UltrapassaLimite(exposicao));
}