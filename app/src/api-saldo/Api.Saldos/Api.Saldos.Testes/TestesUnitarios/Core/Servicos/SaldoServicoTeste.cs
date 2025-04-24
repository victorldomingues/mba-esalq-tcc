using Api.Saldos.Aplicacao.Servicos;
using Api.Saldos.Dominio.Portas;
using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;
using Moq;
using Shouldly;

namespace Api.Saldos.Testes.TestesUnitarios.Core.Servicos;

public class SaldoServicoTeste
{
    [Fact(DisplayName = "Deve recuperar o saldo do banco de dados")]
    public async Task DeveRecuperarSaldoDoBanco()
    {
        var saldoRepositorioMock = new Mock<ISaldoRepositorio>();
        var saldoCacheRepositorioMock = new Mock<ISaldoCacheRepositorio>();
        saldoRepositorioMock.Setup(x => x.Recuperar(It.IsAny<Guid>()))
            .ReturnsAsync(new Saldo() { AtualizadoEm = DateTime.Now, Valor = 130m });
        
        var saldoServico = new SaldoServico( saldoCacheRepositorioMock.Object, saldoRepositorioMock.Object);
        var saldo = await saldoServico.Recuperar(Guid.NewGuid());

        saldo.ShouldNotBeNull();
        saldoRepositorioMock.Verify(x=>x.Recuperar(It.IsAny<Guid>()), Times.Once);
        saldoCacheRepositorioMock.Verify(x=>x.Recuperar(It.IsAny<Guid>()), Times.Once);
        saldoCacheRepositorioMock.Verify(x=>x.CachearSaldo(It.IsAny<Saldo>(), It.IsAny<Guid>()), Times.Once);
    }
    
    [Fact(DisplayName = "Deve recuperar o saldo do cache")]
    public async Task DeveRecuperarSaldoDoCache()
    {
        var saldoRepositorioMock = new Mock<ISaldoRepositorio>();
        var saldoCacheRepositorioMock = new Mock<ISaldoCacheRepositorio>();
        
        saldoCacheRepositorioMock.Setup(x => x.Recuperar(It.IsAny<Guid>()))
            .ReturnsAsync(new Saldo() { AtualizadoEm = DateTime.Now, Valor = 130m });
        
        var saldoServico = new SaldoServico( saldoCacheRepositorioMock.Object, saldoRepositorioMock.Object);
        var saldo = await saldoServico.Recuperar(Guid.NewGuid());

        saldo.ShouldNotBeNull();
        saldoCacheRepositorioMock.Verify(x=>x.Recuperar(It.IsAny<Guid>()), Times.Once);
        saldoRepositorioMock.Verify(x=>x.Recuperar(It.IsAny<Guid>()), Times.Never);
        saldoCacheRepositorioMock.Verify(x=>x.CachearSaldo(It.IsAny<Saldo>(), It.IsAny<Guid>()), Times.Never);
    }
}