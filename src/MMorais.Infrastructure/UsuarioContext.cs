using MMorais.Domain;
using MMorais.Core.Data;
using MMorais.Core.Messages;
using Microsoft.EntityFrameworkCore;
using MMorais.Core.Communication.Mediator;

namespace MMorais.Infrastructure;

public class UsuarioContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    public UsuarioContext(DbContextOptions<UsuarioContext> options, 
        IMediatorHandler mediatorHandler): base(options)
    {
        _mediatorHandler = mediatorHandler;
    }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioContext).Assembly);
    }
    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }

        var sucesso = await base.SaveChangesAsync() > 0;
        // Usando Mediator Extension para publicar todos os eventos add no contexto
        if (sucesso)
            await _mediatorHandler.PublicarEventos(this);

        return sucesso;
    }
}