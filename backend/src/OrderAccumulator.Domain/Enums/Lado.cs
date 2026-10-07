using System.ComponentModel;

namespace OrderAccumulator.Domain.Enums;

public enum Lado
{
    [Description("Compra")]
    C = 1,
    [Description("Venda")]
    V = 2
}