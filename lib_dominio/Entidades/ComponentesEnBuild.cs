using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class ComponentesEnBuild
    {
        public int Id { get; set; }
        public int BuildId { get; set; }
        public int ComponenteId { get; set; }

        [ForeignKey("BuildId")]
        public Builds? Build { get; set; }

        [ForeignKey("ComponenteId")]
        public Componentes? Componente { get; set; }
    }
}