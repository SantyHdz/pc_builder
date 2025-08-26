using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class BuildsAplicacion : IBuildsAplicacion
    {
        private IConexion? IConexion = null;

        public BuildsAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Builds> Listar()
        {
            return this.IConexion!.Builds!.ToList();
        }

        public Builds? Guardar(Builds? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
            this.IConexion!.Builds!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Builds? Modificar(Builds? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            var entry = this.IConexion!.Entry<Builds>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Builds? Borrar(Builds? entidad)
        {
            if (entidad == null) throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
            this.IConexion!.Builds!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Builds> ObtenerPorUsuario(int usuarioId)
        {
            return this.IConexion!.Builds!.Where(b => b.UsuarioId == usuarioId).ToList();
        }
    }
}