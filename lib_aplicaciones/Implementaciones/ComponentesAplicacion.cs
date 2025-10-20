using lib_aplicaciones.Interfaces;
using lib_dominio.DTOs;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ComponentesAplicacion : IComponentesAplicacion
    {
        private IConexion? IConexion = null;

        public ComponentesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Componentes> Listar()
        {
            return this.IConexion!.Componentes!.ToList();
        }

        public Componentes? Guardar(Componentes? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
            this.IConexion!.Componentes!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Componentes? Modificar(Componentes? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            var entry = this.IConexion!.Entry<Componentes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Componentes? Borrar(Componentes? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            this.IConexion!.Componentes!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Componentes> FiltrarPorTipo(int tipo)
        {
            return this.IConexion!.Componentes!.Where(x => x.Tipo == tipo).ToList();
        }

        public List<Componentes> ObtenerCompatibles(int componenteId)
        {
            // Buscar compatibilidad en AMBAS direcciones
            var compatiblesDirectos = this.IConexion!.Compatibilidad!
                .Where(c => c.ComponenteId == componenteId)
                .Select(c => c.ComponenteCompatibleId)
                .ToList();

            var compatiblesInversos = this.IConexion!.Compatibilidad!
                .Where(c => c.ComponenteCompatibleId == componenteId)
                .Select(c => c.ComponenteId)
                .ToList();

            // Unir ambas listas
            var todosCompatibles = compatiblesDirectos.Union(compatiblesInversos).ToList();

            return this.IConexion.Componentes!
                .Where(c => todosCompatibles.Contains(c.Id))
                .ToList();
        }
        public async Task<IEnumerable<Componentes>> BuscarConFiltrosAsync(FiltroComponenteDto filtro)
        {
            var query = this.IConexion!.Componentes!.AsQueryable();

            if (!string.IsNullOrEmpty(filtro.Marca))
                query = query.Where(c => c.Marca.Contains(filtro.Marca));
            if (filtro.PrecioMin.HasValue)
                query = query.Where(c => c.Precio >= filtro.PrecioMin.Value);
            if (filtro.PrecioMax.HasValue)
                query = query.Where(c => c.Precio <= filtro.PrecioMax.Value);
            if (!string.IsNullOrEmpty(filtro.Tipo))
                query = query.Where(c => c.TipoComponente.Nombre == filtro.Tipo);
            if (!string.IsNullOrEmpty(filtro.Caracteristica))
                query = query.Where(c => EF.Functions.Like(c.Especificaciones, $"%{filtro.Caracteristica}%")); // Asume campo Especificaciones en Componente
            return await query.ToListAsync(); // RNF-01: Query LINQ eficiente para <2s
        }
    }
}
