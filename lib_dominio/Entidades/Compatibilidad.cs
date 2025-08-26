using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Compatibilidad
    {
        public int Id { get; set; }
        public int ComponenteId { get; set; }
        public int ComponenteCompatibleId { get; set; }

        [ForeignKey("ComponenteId")]
        public Componentes? Componente { get; set; }

        [ForeignKey("ComponenteCompatibleId")]
        public Componentes? ComponenteCompatible { get; set; }
    }
}