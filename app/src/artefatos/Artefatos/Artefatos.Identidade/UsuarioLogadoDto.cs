namespace Artefatos.Identidade;

public record UsuarioLogadoDto
{
    public UsuarioLogadoDto()
    {
        
    }
    public UsuarioLogadoDto(Guid id, string email, bool estaLogado = true) 
    {
        Id = id;
        Email = email;
        EstaLogado = estaLogado;    
    }
    
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public bool EstaLogado { get; set; } = false;
};