using Api.Cadastro.Dominio.ClienteAgregado;
using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.Portas;
using Moq;
using Shouldly;

namespace Api.Cadastro.Testes.Construtures;

public class PerfilCacheRepositorioConstrutor
{
    
    public Mock<IPerfilCacheRepositorio> Mock { get; }
    public IPerfilCacheRepositorio Instancia => Mock.Object;

    public PerfilCacheRepositorioConstrutor()
    {
        Mock = new Mock<IPerfilCacheRepositorio>();
    }

    public static PerfilCacheRepositorioConstrutor Construir()
    {
        return new PerfilCacheRepositorioConstrutor().Resetar();
    }

    public PerfilCacheRepositorioConstrutor Resetar()
    {
        Mock.Setup(repositorio => repositorio.Recuperar(It.IsAny<string>()));
        Mock.Setup(repositorio => repositorio.Cachear(It.IsAny<string>(), It.IsAny<PerfilDto>()));

        return this;
    }

    public PerfilCacheRepositorioConstrutor PerfilExistente(PerfilDto? perfil = null)
    {
        perfil ??= new PerfilDto()
        {
            Id = Guid.NewGuid(),
            Nome = "Nome",
            Email = "Email",
            Cpf = "Cpf"
        };

        Mock.Setup(repositorio => repositorio.Recuperar(It.IsAny<string>()))
            .ReturnsAsync(perfil);
        
        return this;
    }
}