namespace lib_dominio.DTOs
{
    public class FiltroComponenteDto
    {
        public string? Marca { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public string? Tipo { get; set; } // e.g., "CPU", "GPU"
        public string? Caracteristica { get; set; } // e.g., "Núcleos" para CPU
    }
}