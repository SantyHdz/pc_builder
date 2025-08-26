using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ITiposComponentesAplicacion
    {
        void Configurar(string StringConexion);
        List<TiposComponentes> Listar();
        TiposComponentes? Guardar(TiposComponentes? entidad);
        TiposComponentes? Modificar(TiposComponentes? entidad);
        TiposComponentes? Borrar(TiposComponentes? entidad);
    }
}