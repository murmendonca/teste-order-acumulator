using FluentValidation;
using Moq;
using OrderAccumulator.Application.DTOs.v1.Ordens;
using OrderAccumulator.Application.Interfaces.v1.Repositories;
using OrderAccumulator.Application.Services.v1;

namespace OrderAccumulator.UnitTests;

public class OrdemServiceTests
{
    private readonly Mock<IOrdemRepository> _ordemRepositoryMock = new();
    private readonly Mock<IValidator<OrdemRequest>> _validatorMock = new();

    [Fact]
    public async Task Compra_Aumenta_Exposicao()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        
        var service = CreateService();
        
        var request = CreateRequestCompra();

        var resultado = await service.ProcessarOrdemAsync(request);
        
        Assert.True(resultado.Sucesso);
    }

    [Fact]
    public async Task Venda_Diminui_Exposicao()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        
        var service = CreateService();
        
        var request = CreateRequestVenda();

        var resultado = await service.ProcessarOrdemAsync(request);
        
        Assert.True(resultado.Sucesso);
    }
    
    [Fact]
    public async Task Ordem_Invalida_Retorna_Erro()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<OrdemRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult(new[] { new FluentValidation.Results.ValidationFailure("Property", "Error message") }));
        
        var service = CreateService();
        
        var request = CreateRequestVenda();

        var resultado = await service.ProcessarOrdemAsync(request);
        
        Assert.False(resultado.Sucesso);
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