using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class TiposComponentesAplicacion : ITiposComponentesAplicacion
    {
        private IConexion? IConexion = null;

        public TiposComponentesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<TiposComponentes> Listar()
        {
            return this.IConexion!.TiposComponentes!.ToList();
        }

        public TiposComponentes? Guardar(TiposComponentes? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
            this.IConexion!.TiposComponentes!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public TiposComponentes? Modificar(TiposComponentes? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            var entry = this.IConexion!.Entry<TiposComponentes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public TiposComponentes? Borrar(TiposComponentes? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            this.IConexion!.TiposComponentes!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}