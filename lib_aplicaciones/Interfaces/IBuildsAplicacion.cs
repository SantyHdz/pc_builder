using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IBuildsAplicacion
    {
        void Configurar(string StringConexion);
        List<Builds> Listar();
        Builds? Guardar(Builds? entidad);
        Builds? Modificar(Builds? entidad);
        Builds? Borrar(Builds? entidad);
        List<Builds> ObtenerPorUsuario(int usuarioId);
    }
}