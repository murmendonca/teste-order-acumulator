using OrderAccumulator.Domain.Enums;

namespace OrderAccumulator.Domain.Entities.v1;

public class Ordem
{
    public int Id { get; private set; }
    public Lado Lado { get; private set; }
    public Ativo Ativo { get; private set; }
    public int Quantidade { get; private set; }
    public decimal Preco { get; private set; }
    public DateTime CriadoEm { get; private set; }
    
    private Ordem() { }
    
    public Ordem(Lado lado, Ativo ativo, int quantidade, decimal preco)
    {
        Lado = lado;
        Ativo = ativo;
        Quantidade = quantidade;
        Preco = preco;
        CriadoEm = DateTime.UtcNow;
    }

    private decimal Valor => Preco * Quantidade;
    
    public decimal ValorExposicao => Lado == Lado.C ? Valor : -Valor;
}