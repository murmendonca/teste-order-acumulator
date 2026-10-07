using FluentValidation;
using Moq;
using OrderAccumulator.Application.DTOs.v1.Ordens;
using OrderAccumulator.Application.Interfaces.v1.Repositories;
using OrderAccumulator.Application.Services.v1;
using OrderAccumulator.Domain.Entities.v1;
using OrderAccumulator.Domain.Enums;

namespace OrderAccumulator.UnitTests.Services.v1;

public class OrdemServiceTests
{
    private readonly Mock<IOrdemRepository> _ordemRepositoryMock = new();
    private readonly Mock<IValidator<OrdemRequest>> _validatorMock = new();

    [Fact]
    public async Task COMPRA_AUMENTA_EXPOSICAO()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        
        var service = CreateService();
        
        var request = CreateRequestCompra();

        var resultado = await service.ProcessarOrdemAsync(request);
        
        Assert.True(resultado.Sucesso);
    }

    [Fact]
    public async Task VENDA_DIMINUI_EXPOSICAO()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        
        var service = CreateService();
        
        var request = CreateRequestVenda();

        var resultado = await service.ProcessarOrdemAsync(request);
        
        Assert.True(resultado.Sucesso);
    }
    
    [Fact]
    public async Task ORDEM_INVALIDA_RETORNA_ERRO()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult(new[] { new FluentValidation.Results.ValidationFailure("Property", "Error message") }));
        
        var service = CreateService();
        
        var request = CreateRequestVenda();

        var resultado = await service.ProcessarOrdemAsync(request);
        
        Assert.False(resultado.Sucesso);
    }
    
    [Fact]
    public async Task ORDEM_QUE_ATINGE_O_LIMITE_E_PROCESSA()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _ordemRepositoryMock.Setup(r => r.GetExposicaoPorAtivoAsync(Ativo.PETR4))
            .ReturnsAsync(999_000m);

        var service = CreateService();
        
        var resultado = await service.ProcessarOrdemAsync(new OrdemRequest("PETR4", "C", 100, 10m));

        Assert.True(resultado.Sucesso);
        Assert.Equal(1_000_000m, resultado.ExposicaoAtual);
        _ordemRepositoryMock.Verify(r => r.AddOrdemAsync(It.IsAny<Ordem>()), Times.Once);
    }

    [Fact]
    public async Task ORDEM_QUE_ULTRAPASSA_O_LIMITE_E_NAO_PROCESSA()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _ordemRepositoryMock.Setup(r => r.GetExposicaoPorAtivoAsync(Ativo.PETR4))
            .ReturnsAsync(999_000m);

        var service = CreateService();
        
        var resultado = await service.ProcessarOrdemAsync(new OrdemRequest("PETR4", "C", 100, 10.01m));

        Assert.False(resultado.Sucesso);
        Assert.Equal(999_000m, resultado.ExposicaoAtual);
        Assert.NotNull(resultado.MensagemErro);
        _ordemRepositoryMock.Verify(r => r.AddOrdemAsync(It.IsAny<Ordem>()), Times.Never);
    }

    [Fact]
    public async Task ORDEM_QUE_ULTRAPASSA_O_LIMITE_NEGATIVO_E_NAO_PROCESSA()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _ordemRepositoryMock.Setup(r => r.GetExposicaoPorAtivoAsync(Ativo.PETR4))
            .ReturnsAsync(-999_000m);

        var service = CreateService();
        
        var resultado = await service.ProcessarOrdemAsync(new OrdemRequest("PETR4", "V", 100, 10.01m));

        Assert.False(resultado.Sucesso);
        Assert.Equal(-999_000m, resultado.ExposicaoAtual);
        _ordemRepositoryMock.Verify(r => r.AddOrdemAsync(It.IsAny<Ordem>()), Times.Never);
    }

    [Fact]
    public async Task VENDA_QUE_REDUZ_EXPOSICAO_NO_LIMITE_E_PROCESSA()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _ordemRepositoryMock.Setup(r => r.GetExposicaoPorAtivoAsync(Ativo.PETR4))
            .ReturnsAsync(1_000_000m);

        var service = CreateService();
        
        var resultado = await service.ProcessarOrdemAsync(new OrdemRequest("PETR4", "V", 100, 10m));

        Assert.True(resultado.Sucesso);
        Assert.Equal(999_000m, resultado.ExposicaoAtual);
        _ordemRepositoryMock.Verify(r => r.AddOrdemAsync(It.IsAny<Ordem>()), Times.Once);
    }

    private OrdemService CreateService()
    {
        return new OrdemService(_validatorMock.Object, _ordemRepositoryMock.Object);
    }

    private static OrdemRequest CreateRequestCompra()
    {
        return new OrdemRequest("PETR4", "C", 100, 10m);
    }

    private static OrdemRequest CreateRequestVenda()
    {
        return new OrdemRequest("PETR4", "V", 100, 10m);
    }
}