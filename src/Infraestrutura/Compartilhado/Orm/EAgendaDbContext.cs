using eAgenda.Dominio.Modulos.Categorias;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Compartilhado.Orm;

public sealed class EAgendaDbContext(DbContextOptions<EAgendaDbContext> options) : DbContext(options)
{
    public DbSet<Categoria> Categorias => Set<Categoria>();

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EAgendaDbContext).Assembly);
    }
}
