using Microsoft.EntityFrameworkCore;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
namespace Api.Movimentacoes.Infra.Dados.Contextos;

public class PostgresDbContexto : DbContext
    {
    public PostgresDbContexto(DbContextOptions<PostgresDbContexto> options)
        : base(options)
    {
    }
    
    public DbSet<Movimentacao> Movimentacoes { get; set; }
    public DbSet<Saldo> Saldos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Movimentacao>(
            entidade =>
            {
                entidade.ToTable("movimentacoes");
                entidade.HasKey(x=>x.Id);
            });
        modelBuilder.Entity<Saldo>(
            entidade =>
            {
                entidade.ToTable("saldos");
                entidade.HasKey(x=>x.IdUsuario);
            });
        
    }
}