using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IComponentesAplicacion
    {
        void Configurar(string StringConexion);
        List<Componentes> Listar();
        Componentes? Guardar(Componentes? entidad);
        Componentes? Modificar(Componentes? entidad);
        Componentes? Borrar(Componentes? entidad);
        List<Componentes> FiltrarPorTipo(int tipo);
        List<Componentes> ObtenerCompatibles(int componenteId);
    }
}