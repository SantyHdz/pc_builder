using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Builds
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = "";
        public decimal PrecioTotal { get; set; }
        public int ConsumoEnergeticoTotal { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [ForeignKey("UsuarioId")]
        public Usuarios? Usuario { get; set; }

        public virtual ICollection<ComponentesEnBuild>? ComponentesEnBuilds { get; set; }
    }
}