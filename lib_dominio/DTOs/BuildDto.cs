using lib_dominio.Entidades;

namespace lib_dominio.DTOs
{
    public class BuildDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public List<ComponentesEnBuild> Componentes { get; set; } = new();
        public decimal PrecioTotal { get; set; }
        public int ConsumoEstimadoW { get; set; } // Cálculo basado en specs de componentes
        public string? EnlaceCompartir { get; set; } // Para RU-08/RF-09
    }

    public class BuildResponseDto : BuildDto
    {
        public string PdfUrl { get; set; } = string.Empty; // Para RU-07/RF-08
    }
}