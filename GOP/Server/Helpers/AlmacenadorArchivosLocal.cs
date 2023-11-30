namespace GOP.Server.Helpers
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string nombreContenedor, string ruta)
        {
            if (!String.IsNullOrEmpty(ruta))
            {
                await EliminarArchivo(ruta);
            }

            return await GuardarArchivo(contenido, extension, nombreContenedor);
        }

        public Task EliminarArchivo(string ruta)
        {
            var filename = Path.GetFileName(ruta);
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }

            return Task.FromResult(0);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor)
        {
            var filename = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, nombreContenedor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string rutaGuardado = Path.Combine(folder, filename);
            await File.WriteAllBytesAsync(rutaGuardado, contenido);

            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var rutaParaBD = Path.Combine(urlActual, nombreContenedor, filename);

            return rutaParaBD;
        }
    }
}
