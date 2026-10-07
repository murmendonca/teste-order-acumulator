namespace OrderAccumulator.Application.DTOs.v1;

public record Resultado
{
    public bool Sucesso { get; init; }
    public decimal ExposicaoAtual { get; init; }
    public string? Mensagem { get; init; }
    
    public Resultado Ok(decimal exposicaoAtual)
    {
        return new Resultado
        {
            Sucesso = true,
            ExposicaoAtual = exposicaoAtual,
            Mensagem = null
        };
    }
    
    public Resultado Erro(decimal exposicaoAtual, string mensagem)
    {
        return new Resultado
        {
            Sucesso = false,
            ExposicaoAtual = exposicaoAtual,
            Mensagem = mensagem
        };
    }
}