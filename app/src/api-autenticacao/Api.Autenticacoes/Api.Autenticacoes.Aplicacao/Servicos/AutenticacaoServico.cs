using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Autenticacoes.Aplicacao.Modelos;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosTransferencia;
using Api.Autenticacoes.Dominio.Portas;
using Artefatos.Notificacoes;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Api.Autenticacoes.Aplicacao.Servicos;

public class AutenticacaoServico : IAutenticacaoServico
{
    private readonly byte[] _chave;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessaoCacheRepositorio _sessaoCacheRepositorio;
    private readonly INotificacaoContexto _notificacaoContext;
    private readonly ILogger<AutenticacaoServico> _logger;
    public AutenticacaoServico(ILogger<AutenticacaoServico> logger, IUsuarioRepositorio usuarioRepositorio, INotificacaoContexto notificacaoContext, 
        ISessaoCacheRepositorio sessaoCacheRepositorio,
        OpcoesDeSeguranca opcoesDeSeguranca)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _notificacaoContext = notificacaoContext;
        _sessaoCacheRepositorio = sessaoCacheRepositorio;
        _chave = opcoesDeSeguranca.Chave;
        _logger = logger;
    }

    public async Task<Sessao?> Logar(LoginDto login)
    {
        var usuario = await _usuarioRepositorio.RecuperarUsuarioAsync(login);
         
        if (usuario == null || !usuario.SenhaValida(login.Senha))
        {
            // TODO: CODIGO DE ERRO ADEQUADO
            _notificacaoContext.AdicionarNotificacao(new Notificacao("AUTENTICACAO_ERRO_001", 
                $"Ocorreu um proplema, mandamos as orientações para o seu e-mail cadastrado."));
            return null;
        }
        
        var sessao = CriarSessao(usuario);
        
        await _sessaoCacheRepositorio.CachearSessaoAsync(sessao);
        
        return sessao;
    }
    
    public ClaimsPrincipal? ValidarToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = CriarParametrosDeValidacao();

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao validar loken: {ex.Message}");
            _notificacaoContext.AdicionarNotificacao(new Notificacao("token", "token invalido"));
            return new ClaimsPrincipal();
        }
    }

    private Sessao CriarSessao(Usuario usuario)
    {
        var criadaEm = DateTime.UtcNow;
        var expiraEm = criadaEm.AddMinutes(40);
        var token = CriarToken(usuario, expiraEm);
        var sessao = new Sessao(usuario.Id, criadaEm, expiraEm, token);
        return sessao;
    }

    private string CriarToken(Usuario usuario , DateTime expiraEm)
    {
        var jwtTokenManipulador = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, usuario.Email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiraEm,
            Issuer = "https://localhost:7243/",
            Audience = "https://localhost:7243/",
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(_chave), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtTokenManipulador.CreateToken(tokenDescriptor);
        return jwtTokenManipulador.WriteToken(token);
    }
    
    private TokenValidationParameters CriarParametrosDeValidacao()
    {
        return new TokenValidationParameters()
        {
            ValidateLifetime = false, 
            ValidateAudience = false, 
            ValidateIssuer = false,   
            ValidIssuer = "https://localhost:7243/",
            ValidAudience = "https://localhost:7243/",
            IssuerSigningKey = new SymmetricSecurityKey(_chave) 
        };
    }
}