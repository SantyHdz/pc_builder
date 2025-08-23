using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Componentes
    {
        [Key]
        public int IdComponente { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public string Especificaciones { get; set; }
        [ForeignKey("IdCategoria")] public Categorias? _Categoria { get; set; }
        
    }
}