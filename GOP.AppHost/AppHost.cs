var builder = DistributedApplication.CreateBuilder(args);

// Aspire detecta automáticamente los puertos desde launchSettings.json del Server
var server = builder.AddProject<Projects.GOP_Server>("gop-server");

// El cliente se sirve desde el servidor (UseBlazorFrameworkFiles + MapFallbackToFile en
// GOP.Server/Program.cs), no se ejecuta como resource aparte: si se agregaba acá arriba,
// competía por los mismos puertos del launchSettings.json del Server (7297/5297) y las
// requests terminaban en el host standalone de WebAssembly del Client, que no tiene los
// controllers de la API.

builder.Build().Run();
