using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ComponentesEnBuildAplicacion : IComponentesEnBuildAplicacion
    {
        private IConexion? IConexion = null;

        public ComponentesEnBuildAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<ComponentesEnBuild> Listar()
        {
            return this.IConexion!.ComponentesEnBuild!.ToList();
        }

        public ComponentesEnBuild? Guardar(ComponentesEnBuild? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
            this.IConexion!.ComponentesEnBuild!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ComponentesEnBuild? Modificar(ComponentesEnBuild? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            var entry = this.IConexion!.Entry<ComponentesEnBuild>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ComponentesEnBuild? Borrar(ComponentesEnBuild? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            this.IConexion!.ComponentesEnBuild!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<ComponentesEnBuild> ObtenerPorBuild(int buildId)
        {
            return this.IConexion!.ComponentesEnBuild!.Where(c => c.BuildId == buildId).ToList();
        }
    }
}
