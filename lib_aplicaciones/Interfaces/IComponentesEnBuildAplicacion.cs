using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IComponentesEnBuildAplicacion
    {
        void Configurar(string StringConexion);
        List<ComponentesEnBuild> Listar();
        ComponentesEnBuild? Guardar(ComponentesEnBuild? entidad);
        ComponentesEnBuild? Modificar(ComponentesEnBuild? entidad);
        ComponentesEnBuild? Borrar(ComponentesEnBuild? entidad);
        List<ComponentesEnBuild> ObtenerPorBuild(int buildId);
    }
}