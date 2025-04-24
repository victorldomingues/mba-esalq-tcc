using Api.Cadastro.Dominio.ClienteAgregado;
using Api.Cadastro.Dominio.Portas;

namespace Api.Cadastro.Aplicacao.Servicos;

public class PerfilServico : IPerfilServico
{
    private readonly IPerfilRepositorio _perfilRepositorio;
    private readonly IPerfilCacheRepositorio _perfilCacheRepositorio;

    public PerfilServico(IPerfilRepositorio perfilRepositorio, IPerfilCacheRepositorio perfilCacheRepositorio)
    {
        _perfilRepositorio = perfilRepositorio;
        _perfilCacheRepositorio = perfilCacheRepositorio;
    }

    public async Task<PerfilDto> Recuperar(Guid id)
    {
        var chave = ComporChave(id);
        var perfil = await _perfilCacheRepositorio.Recuperar(chave);

        if (perfil != null) return perfil;

        perfil = await _perfilRepositorio.Recuperar(id);
        await _perfilCacheRepositorio.Cachear(chave, perfil);

        return perfil;
    }

    private string ComporChave(Guid id) => $"perfil-{id}";
}