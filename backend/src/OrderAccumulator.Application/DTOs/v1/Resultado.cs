using System.Text.Json.Serialization;

namespace OrderAccumulator.Application.DTOs.v1;

public class Resultado
{
    [JsonPropertyName("sucesso")]
    public bool Sucesso { get; init; }
    [JsonPropertyName("exposicao_atual")]
    public decimal ExposicaoAtual { get; init; }
    [JsonPropertyName("msg_erro")]
    public string? MensagemErro { get; init; }
    
    public Resultado Ok(decimal exposicaoAtual)
    {
        return new Resultado
        {
            Sucesso = true,
            ExposicaoAtual = exposicaoAtual,
            MensagemErro = null
        };
    }
    
    public Resultado Erro(decimal exposicaoAtual, string mensagem)
    {
        return new Resultado
        {
            Sucesso = false,
            ExposicaoAtual = exposicaoAtual,
            MensagemErro = mensagem
        };
    }
}