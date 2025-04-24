using Api.Saldos.Dominio.Portas;
using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;

namespace Api.Saldos.Aplicacao.Servicos;

public class SaldoServico : ISaldoServico
{
    private readonly ISaldoRepositorio _saldoRepositorio;
    private readonly ISaldoCacheRepositorio _saldoCacheRepositorio;

    public SaldoServico(ISaldoCacheRepositorio saldoCacheRepositorio, ISaldoRepositorio saldoRepositorio)
    {
        _saldoCacheRepositorio = saldoCacheRepositorio;
        _saldoRepositorio = saldoRepositorio;
    }

    public  async Task<Saldo> Recuperar(Guid idUsuario)
    {
        var saldo = await _saldoCacheRepositorio.Recuperar(idUsuario);

        if (saldo != null) return saldo;
        
        saldo = await _saldoRepositorio.Recuperar(idUsuario);
        
        await _saldoCacheRepositorio.CachearSaldo(saldo, idUsuario);

        return saldo;
    }


}