using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Api.Autenticacoes.Dados.Contextos;

public class PostgresDbContexto : DbContext
{
    public PostgresDbContexto(DbContextOptions<PostgresDbContexto> options)
        : base(options)
    {
    }
    
    public DbSet<Usuario> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Usuario>(
            entidade =>
            {
                entidade.ToTable("usuarios");
                entidade.HasKey(x=>x.Id);
            });
    }
}