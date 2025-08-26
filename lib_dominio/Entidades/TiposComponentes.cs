using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades;

public class TiposComponentes
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}