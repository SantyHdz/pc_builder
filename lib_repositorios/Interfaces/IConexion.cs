using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace lib_repositorios.Interfaces;

public interface IConexion
{
    string? StringConexion { get; set; }

    DbSet<ComponentesEnBuild>? ComponentesEnBuild { get; set; }
    DbSet<Builds>? Builds { get; set; }
    DbSet<Compatibilidad>? Compatibilidad { get; set; }
    DbSet<Componentes>? Componentes { get; set; }
    DbSet<TiposComponentes>? TiposComponentes { get; set; }
    DbSet<Auditoria>? Auditorias { get; set; }
    DbSet<Roles>? Roles { get; set; }
    DbSet<Usuarios>? Usuarios { get; set; }
    EntityEntry<T> Entry<T>(T entity) where T : class;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}