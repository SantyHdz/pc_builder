using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json; // 👈 Importante para usar [JsonIgnore]

namespace lib_dominio.Entidades
{
    public class ComponentesEnBuild
    {
        public int Id { get; set; }
        public int BuildId { get; set; }
        public int ComponenteId { get; set; }
        
        [ForeignKey("BuildId")]
        [JsonIgnore]
        public Builds? Build { get; set; }

        [JsonIgnore]
        [ForeignKey("ComponenteId")]
        public Componentes? Componente { get; set; }
    }
}