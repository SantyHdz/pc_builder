using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using lib_aplicaciones.Interfaces;
using lib_dominio.DTOs;
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
            return this.IConexion!.Builds!
                .Where(b => b.UsuarioId == usuarioId)
                .Include(b => b.ComponentesEnBuilds!)
                .ThenInclude(cb => cb.Componente)
                .ToList();
        }

        public async Task<BuildDto> CalcularBuildAsync(Builds build)
        {
            var dto = new BuildDto
            {
                Id = build.Id,
                UsuarioId = build.UsuarioId,
                Componentes = build.ComponentesEnBuilds?.ToList() ?? new List<ComponentesEnBuild>()
            };

            dto.PrecioTotal = dto.Componentes.Sum(cb => (cb.Componente?.Precio ?? 0) * 1); // Ajustar si usas Cantidad
            dto.ConsumoEstimadoW = dto.Componentes.Sum(cb => (cb.Componente?.ConsumoEnergetico ?? 0) * 1);

            return await Task.FromResult(dto); // Mantengo async para compatibilidad
        }

        public async Task<BuildDto> GuardarBuildAsync(BuildDto buildDto, int usuarioId)
        {
            var build = new Builds
            {
                UsuarioId = usuarioId,
                Nombre = $"Build de {usuarioId}",
                ComponentesEnBuilds = buildDto.Componentes
            };

            this.IConexion!.Builds!.Add(build);
            await this.IConexion.SaveChangesAsync();

            buildDto.Id = build.Id;
            return await CalcularBuildAsync(build);
        }

        public async Task<string> GenerarPdfAsync(BuildDto buildDto)
        {
            var pdfDir = Path.Combine("wwwroot", "pdfs");
            if (!Directory.Exists(pdfDir))
                Directory.CreateDirectory(pdfDir);

            var pdfPath = Path.Combine(pdfDir, $"build_{buildDto.Id}.pdf");

            using var writer = new PdfWriter(pdfPath);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            document.Add(new Paragraph("Configuración PC"));
            document.Add(new Paragraph($"Precio Total: {buildDto.PrecioTotal:C}"));
            document.Add(new Paragraph($"Consumo: {buildDto.ConsumoEstimadoW}W"));

            foreach (var comp in buildDto.Componentes)
            {
                var marca = comp.Componente?.Marca ?? "Desconocida";
                var modelo = comp.Componente?.Nombre ?? "Sin nombre";
                document.Add(new Paragraph($"{marca} {modelo}"));
            }

            return await Task.FromResult(pdfPath);
        }

        public async Task<string> GenerarEnlaceCompartirAsync(int buildId)
        {
            var build = await this.IConexion!.Builds!.FindAsync(buildId);
            if (build == null) throw new Exception("Build no encontrada");

            var enlace = $"https://midominio.com/build/{Guid.NewGuid()}";
            return enlace;
        }
    }
}
