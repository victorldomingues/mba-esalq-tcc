using Api.Cadastro.Dominio.ClienteAgregado;
using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.Portas;
using Moq;

namespace Api.Cadastro.Testes.Construtures;

public class PerfilRepositorioConstrutor
{
    
    public Mock<IPerfilRepositorio> Mock { get; }
    public IPerfilRepositorio Instancia => Mock.Object;

    public PerfilRepositorioConstrutor()
    {
        Mock = new Mock<IPerfilRepositorio>();
    }

    public static PerfilRepositorioConstrutor Construir()
    {
        return new PerfilRepositorioConstrutor().Resetar();
    }

    public PerfilRepositorioConstrutor Resetar()
    {
        Mock.Setup(repositorio => repositorio.Recuperar(It.IsAny<Guid>()));

        return this;
    }

    public PerfilRepositorioConstrutor PerfilExistente(PerfilDto? perfil = null)
    {
        perfil ??= new PerfilDto()
        {
            Id = Guid.NewGuid(),
            Nome = "Nome",
            Email = "Email",
            Cpf = "Cpf"
        };

        Mock.Setup(repositorio => repositorio.Recuperar(It.IsAny<Guid>()))
            .ReturnsAsync(perfil);
        
        return this;
    }
}