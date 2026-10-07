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
    
    public static Resultado Ok(decimal exposicaoAtual) => new()
    {
        Sucesso = true,
        ExposicaoAtual = exposicaoAtual,
        MensagemErro = null
    };

    public static Resultado Erro(decimal exposicaoAtual, string mensagem) => new()
    {
        Sucesso = false,
        ExposicaoAtual = exposicaoAtual,
        MensagemErro = mensagem
    };
}