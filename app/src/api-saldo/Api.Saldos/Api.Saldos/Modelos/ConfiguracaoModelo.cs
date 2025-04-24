using System.Diagnostics.CodeAnalysis;

namespace Api.Saldos.Modelos;

[ExcludeFromCodeCoverage]
public class ConfiguracaoModelo
{
    public bool? AcrescimoDeLatencia { get; set; } = false;
    public bool? ProvocarErro { get; set; } = false;
}