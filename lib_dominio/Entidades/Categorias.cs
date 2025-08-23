using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Categorias
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
    }
}