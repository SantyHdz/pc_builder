using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Builds
    {
        [Key]
        public int IdBuild { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        [ForeignKey("IdUsuario")] public Usuarios? _Usuario { get; set; }
    }
}