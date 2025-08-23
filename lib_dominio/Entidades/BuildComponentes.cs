using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class BuildComponentes
    {
        [Key]
        public int IdBuildComponente { get; set; }
        public int IdBuild { get; set; }
        public int IdComponente { get; set; }
        
        [ForeignKey("IdBuild")]
        public Builds? _Build { get; set; }

        [ForeignKey("IdComponente")]
        public Componentes? _Componente { get; set; }
    }
}