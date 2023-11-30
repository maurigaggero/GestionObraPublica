using GOP.Shared.ENUM;

namespace GOP.BD.Data
{
    public interface IEntidadBase
    {
        EnumEstadoRegistro EstadoRegistro { get; set; }
        DateTime FechaCrea { get; set; }
        DateTime FechaModif { get; set; }
        int Id { get; set; }
        string IdCrea { get; set; }
        string IdModif { get; set; }
        string? Observacion { get; set; }
        string IdBaja { get; set; }
        DateTime FechaBaja { get; set; }
    }
}