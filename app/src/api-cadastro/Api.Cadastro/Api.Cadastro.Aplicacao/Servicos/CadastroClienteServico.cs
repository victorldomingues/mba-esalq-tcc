using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.Portas;
using Artefatos.Notificacoes;

namespace Api.Cadastro.Aplicacao.Servicos;

public class CadastroClienteServico : ICadastroClienteServico
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly INotificacaoContexto _notificacaoContexto;

    public CadastroClienteServico(IUsuarioRepositorio usuarioRepositorio, INotificacaoContexto notificacaoContexto)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _notificacaoContexto = notificacaoContexto;
    }

    public async Task Cadastrar(Usuario usuario)
    {
        var usuarioExistente = await _usuarioRepositorio.Recuperar(usuario.Cpf.Numero, usuario.Email);
        
        if (usuarioExistente != null)
        {
            _notificacaoContexto.AdicionarNotificacao(new Notificacao("ERRO_CLIENTE_1001",
                "Ocorreu um erro ao cadastrar o cliente e-mail ou cpf já cadastrado. "));
            return;
        }
        
        await _usuarioRepositorio.Cadastrar(usuario);
    }
    
}