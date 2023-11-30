namespace GOP.Server.Helpers
{
    public interface IAlmacenadorArchivos
    {
        Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor);
        Task EliminarArchivo(string ruta);
        Task<string> EditarArchivo(byte[] contenido, string extension, string nombreContenedor, string ruta);
    }
}
