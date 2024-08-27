using GOP.Client;
using GOP.Client.Services;
using GOP.Client.Services.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<UnidadesService>()
                .AddScoped<ItemsService>()
                .AddScoped<ItemControlesService>()
                .AddScoped<ItemsControlParamsService>()
                .AddScoped<ItemControlDocsService>()
                .AddScoped<EmpresasService>()
                .AddScoped<ZonasService>()
                .AddScoped<PersonaService>()
                .AddScoped<EmpresaProfesionalesService>()
                .AddScoped<ZonaProfesionalesService>()
                .AddScoped<FrentesObraService>()
                .AddScoped<FrenteObraProfesionalesService>()
                .AddScoped<ContratoService>()
                .AddScoped<ContratoItemService>()
                .AddScoped<ContratoItemControlesService>()
                .AddScoped<ContratoItemControlesDocsService>()
                .AddScoped<ContratoItemControlesParamsService>()
                .AddScoped<CalleService>()
                .AddScoped<EstructuraService>()
                .AddScoped<ContratoEstructuraService>()
                .AddScoped<ContratoDocsService>()
                .AddScoped<ContratoEstructuraDocsService>()
                .AddScoped<CertificadoService>()
                .AddScoped<CertificadoDocsService>()
                .AddScoped<CertificadoItemsService>()
                .AddScoped<CertificadoItemDefService>()
                .AddScoped<CertificadoItemControlsService>()
                .AddScoped<CertificadoItemControlDocsService>()
                .AddScoped<CertificadoItemControlParamsService>()
                .AddScoped<EventoTipoService>()
                .AddScoped<EventoService>()
                .AddScoped<EventoDocsService>()
                .AddScoped<UserService>()
                .AddScoped<RenovadorToken>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, UserService>(
    provider => provider.GetRequiredService<UserService>());

await builder.Build().RunAsync();
