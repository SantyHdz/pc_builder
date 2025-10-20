using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Componentes
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; } = "";
        public int? Tipo { get; set; }
        public string? Marca { get; set; } = "";
        public decimal? Precio { get; set; }
        public int? ConsumoEnergetico { get; set; } // en vatios
        public string? Especificaciones { get; set; } = "";
        public string? Imagen { get; set; } = "";
        public string? Socket { get; set; } = ""; // Para CPUs
        public string? TipoRAM { get; set; } = ""; // DDR4, DDR5, etc.
        public string? Formato { get; set; } = ""; // ATX, MicroATX, etc.

        // Propiedad de navegación para la relación con TiposComponentes
        [ForeignKey("Tipo")]
        public TiposComponentes? TipoComponente { get; set; }

        public virtual ICollection<Compatibilidad>? Compatibilidades { get; set; }
        public virtual ICollection<ComponentesEnBuild>? ComponentesEnBuilds { get; set; }
    }
}