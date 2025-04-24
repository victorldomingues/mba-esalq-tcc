using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;
using Microsoft.EntityFrameworkCore;

namespace Api.Cadastro.Data.Contextos;

public class PostgresDbContext : DbContext
    {
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
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
                entidade.OwnsOne<Cpf>(usuario=>usuario.Cpf)
                    .Property(cpf=>cpf.Numero).HasColumnName("cpf");

                entidade.OwnsOne<Cpf>(usuario => usuario.Cpf)
                    .Ignore(cpf => cpf.Valido);
                
                entidade.OwnsOne<Senha>(x => x.Senha)
                    .Property(senha=>senha.Encriptada).HasColumnName("senha");
            });
    }
}