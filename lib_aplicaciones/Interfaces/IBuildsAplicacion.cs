using lib_dominio.DTOs;
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
        Task<BuildDto> CalcularBuildAsync(Builds build);
        // Nuevo para RU-06/RF-07: Guardado con usuario
        Task<BuildDto> GuardarBuildAsync(BuildDto buildDto, int usuarioId);
        // Nuevo para RU-07/RF-08: PDF (retorna path o base64)
        Task<string> GenerarPdfAsync(BuildDto buildDto);
        // Nuevo para RU-08/RF-09: Compartir (genera enlace único, e.g., GUID)
        Task<string> GenerarEnlaceCompartirAsync(int buildId);
    }
}