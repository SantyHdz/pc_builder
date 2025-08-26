using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class CompatibilidadAplicacion : ICompatibilidadAplicacion
    {
        private IConexion? IConexion = null;

        public CompatibilidadAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Compatibilidad> Listar()
        {
            return this.IConexion!.Compatibilidad!.ToList();
        }

        public Compatibilidad? Guardar(Compatibilidad? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
            this.IConexion!.Compatibilidad!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Compatibilidad? Modificar(Compatibilidad? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            var entry = this.IConexion!.Entry<Compatibilidad>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Compatibilidad? Borrar(Compatibilidad? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            this.IConexion!.Compatibilidad!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Compatibilidad> ObtenerCompatibilidadPorComponente(int componenteId)
        {
            return this.IConexion!.Compatibilidad!.Where(c => c.ComponenteId == componenteId).ToList();
        }
    }
}
