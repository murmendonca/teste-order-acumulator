namespace OrderAccumulator.Application.DTOs.v1.Ordens;

public record OrdemRequest(string Ativo, string Lado, int Quantidade, decimal Preco);