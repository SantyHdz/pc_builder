namespace lib_dominio.DTOs
{
    public class EstadisticasAdminDto
    {
        public int TotalBuilds { get; set; }
        public int TotalUsuarios { get; set; }
        public decimal PromedioPrecio { get; set; }
        public List<string> BuildsPopulares { get; set; } = new(); // e.g., IDs o nombres
    }
}