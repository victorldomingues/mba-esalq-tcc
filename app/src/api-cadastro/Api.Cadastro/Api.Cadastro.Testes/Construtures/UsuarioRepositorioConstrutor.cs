using Api.Cadastro.Data.Repositorios;
using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.Portas;
using Moq;

namespace Api.Cadastro.Testes.Construtures;

public class UsuarioRepositorioConstrutor
{
    public Mock<IUsuarioRepositorio> Mock { get; }
    public IUsuarioRepositorio Instancia => Mock.Object;

    public UsuarioRepositorioConstrutor()
    {
        Mock = new Mock<IUsuarioRepositorio>();
    }

    public static UsuarioRepositorioConstrutor Construir()
    {
        return new UsuarioRepositorioConstrutor().Resetar();
    }

    public UsuarioRepositorioConstrutor Resetar()
    {
        Mock.Setup(repositorio => repositorio.Recuperar(It.IsAny<Guid>()));

        Mock.Setup(repositorio => repositorio.Recuperar(It.IsAny<string>(), It.IsAny<string>()));

        Mock.Setup(repositorio => repositorio.Cadastrar(It.IsAny<Usuario>()));

        return this;
    }

    public UsuarioRepositorioConstrutor UsuarioExistente(Usuario? usuario = null)
    {
        usuario ??= UsuarioBuilder.Construir().Instancia;

        Mock.Setup(repositorio => repositorio.Recuperar(It.IsAny<Guid>()))
            .ReturnsAsync(usuario);

        Mock.Setup(repositorio => repositorio.Recuperar(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(usuario);
        
        return this;
    }


}