using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ICompatibilidadAplicacion
    {
        void Configurar(string StringConexion);
        List<Compatibilidad> Listar();
        Compatibilidad? Guardar(Compatibilidad? entidad);
        Compatibilidad? Modificar(Compatibilidad? entidad);
        Compatibilidad? Borrar(Compatibilidad? entidad);
        List<Compatibilidad> ObtenerCompatibilidadPorComponente(int componenteId);
    }
}