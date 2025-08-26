using lib_aplicaciones.Interfaces;
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
            var compatibles = this.IConexion!.Compatibilidad!
                .Where(c => c.ComponenteId == componenteId)
                .Select(c => c.ComponenteCompatibleId)
                .ToList();

            return this.IConexion.Componentes!.Where(c => compatibles.Contains(c.Id)).ToList();
        }
    }
}
