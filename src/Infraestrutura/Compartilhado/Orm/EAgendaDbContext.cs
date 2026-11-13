using eAgenda.Dominio.Modulos.Categorias;
using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Dominio.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Despesas;
using eAgenda.Dominio.Modulos.Tarefas;
using eAgenda.Infraestrutura.Compartilhado.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Compartilhado.Orm;

public sealed class EAgendaDbContext(
    DbContextOptions<EAgendaDbContext> options) : 
    IdentityDbContext<UsuarioIdentity, IdentityRole<Guid>, Guid>(options)
{
     public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Despesa> Despesas => Set<Despesa>();
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    public DbSet<ItemTarefa> ItensTarefa => Set<ItemTarefa>();
    public DbSet<Contato> Contatos => Set<Contato>();
    public DbSet<Compromisso> Compromissos => Set<Compromisso>();

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EAgendaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
