using AutoMapper;
using EFCorePeliculas.Servicios;
using GOP.BD.Data;
using GOP.BD.Data.Seeding;
using GOP.Repositorio;
using GOP.Repositorio.Repos;
using GOP.Server.Helpers;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

#region SERVICES
var conn = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<BDContext>(
    opciones => opciones.UseSqlServer(conn,
    sqlServerOptions => sqlServerOptions.UseNetTopologySuite()
    )
);

builder.Services.AddControllersWithViews()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("es-ES") }; // Región fija: España
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddIdentity<GOPUser, IdentityRole>()
                .AddEntityFrameworkStores<BDContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
                .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(builder.Configuration["jwt:llave"])),
        ClockSkew = TimeSpan.Zero
    });

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new OpenApiInfo { Title = "GOP", Version = "v1", Description = "API de Gestión de Obra Pública" };
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Ingrese el token JWT. Ejemplo: Bearer {token}"
        };
        return Task.CompletedTask;
    });
});

// El generador de OpenAPI (MapOpenApi/Scalar) reutiliza estas opciones (Minimal API), NO las de
// AddControllers().AddJsonOptions() de más arriba, así que esto no afecta la serialización real
// de los controllers ni el JSON que consume el Client. Solo recorta, para el schema de la
// documentación, las propiedades de navegación "hacia atrás" que forman ciclos entre DTOs
// (Contrato<->Empresa, Evento<->Contrato, etc.), evitando que el generador de OpenAPI de .NET 10
// explote al recorrer un grafo de tipos mutuamente recursivo.
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        .WithAddedModifier(OpenApiCycleBreaker.RemoveCyclicNavigationProperties);
});

#region INJECTION SERVICES
builder.Services.AddScoped<IRepositorio<IEntidadBase>, Repositorio<IEntidadBase>>();
builder.Services.AddScoped<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
builder.Services.AddSingleton(provider =>
{
    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
    var config = new MapperConfiguration(cfg =>
    {
        // Ser explícito en los mapeos y no permitir mapeos automáticos no configurados
        cfg.AllowNullCollections = true;
        cfg.AllowNullDestinationValues = true;

        cfg.AddProfile(new AutoMapperProfiles(geometryFactory));
    });

    return config.CreateMapper();
});


//persona
builder.Services.AddScoped<IPersonasRepositorio, PersonasRepositorio>();
//MODULO ITEM
builder.Services.AddScoped<IUnidadesRepositorio, UnidadesRespositorio>();
builder.Services.AddScoped<IItemsRepositorio, ItemsRepositorio>();
builder.Services.AddScoped<IItemDocRepositorio, ItemDocRepositorio>();
builder.Services.AddScoped<IItemsControlsRepositorio, ItemsControlsRepositorio>();
builder.Services.AddScoped<IItemsControlsParamsRepositorio, ItemsControlsParamsRepositorio>();
builder.Services.AddScoped<IItemsControlsDocsRepositorio, ItemsControlsDocsRepositorio>();
builder.Services.AddScoped<ICalleRepositorio, CalleRepositorio>();
builder.Services.AddScoped<IEstructuraTipoRepositorio, EstructuraTipoRepositorio>();
//MODULO ORGANIZACIONES
builder.Services.AddScoped<IEmpresasRepositorio, EmpresasRepositorio>();
builder.Services.AddScoped<IEmpresaProfesionalesRepositorio, EmpresaProfesionalesRepositorio>();
builder.Services.AddScoped<IZonasRepositorio, ZonasRepositorio>();
builder.Services.AddScoped<IZonasProfesionalesRepositorio, ZonasProfesionalesRepositorio>();
builder.Services.AddScoped<IFrenteObrasRepositorio, FrenteObrasRepositorio>();
builder.Services.AddScoped<IFrenteObraProfesionalesRepositorio, FrenteObraProfesionalesRepositorio>();
//MODULO CONTRATOS
builder.Services.AddScoped<IContratoRepositorio, ContratoRepositorio>();
builder.Services.AddScoped<IContratoDocRepositorio, ContratoDocRepositorio>();
builder.Services.AddScoped<IContratoEstructuraRepositorio, ContratoEstructuraRepositorio>();
builder.Services.AddScoped<IContratoEstructuraDocRepositorio, ContratoEstructuraDocRepositorio>();
builder.Services.AddScoped<IContratoItemsRepositorio, ContratoItemRepositorio>();
builder.Services.AddScoped<IContratoItemControlsRepositorio, ContratoItemControlsRepositorio>();
builder.Services.AddScoped<IContratoItemControlParamsRepositorio, ContratoItemControlParamsRepositorio>();
builder.Services.AddScoped<IContratoItemControlDocsRepositorio, ContratoItemControlDocsRepositorio>();
//MODULO CERTIFICADOS
builder.Services.AddScoped<ICertificadoRepositorio, CertificadoRepositorio>();
builder.Services.AddScoped<ICertificadoDocRepositorio, CertificadoDocRepositorio>();
builder.Services.AddScoped<ICertificadoItemsRepositorio, CertificadoItemRepositorio>();
builder.Services.AddScoped<ICertificadoItemDefRepositorio, CertificadoItemDefsRespositorio>();
builder.Services.AddScoped<ICertificadoItemControlsRepositorio, CertificadoItemControlsRepositorio>();
builder.Services.AddScoped<ICertificadoItemControlParamsRepositorio, CertificadoItemControlParamsRepositorio>();
builder.Services.AddScoped<ICertificadoItemControlDocsRepositorio, CertificadoItemControlDocsRepositorio>();
//MODULO EVENTOS
builder.Services.AddScoped<IEventoRepositorio, EventoRepositorio>();
builder.Services.AddScoped<IEventoDocRepositorio, EventoDocRepositorio>();
builder.Services.AddScoped<IEventoTipoRepositorio, EventoTipoRepositorio>();
builder.Services.AddScoped<IEventoParamsRepositorio, EventoParamsRepositorio>();
builder.Services.AddScoped<IEventoParamDocsRepositorio, EventoParamDocsRepositorio>();

#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("Admin", politica => politica.RequireClaim("Admin"));
    opciones.AddPolicy("BaseDatos", politica => politica.RequireClaim("BaseDatos"));
    opciones.AddPolicy("HyS", politica => politica.RequireClaim("HyS"));
    opciones.AddPolicy("Zona1", politica => politica.RequireClaim("Zona1"));
    opciones.AddPolicy("Zona2", politica => politica.RequireClaim("Zona2"));
    opciones.AddPolicy("Frente", politica => politica.RequireClaim("Frente"));
    opciones.AddPolicy("Consulta1", politica => politica.RequireClaim("Consulta1"));
    opciones.AddPolicy("Consulta2", politica => politica.RequireClaim("Consulta2"));
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Ejecutar seeding de datos en Development
if (app.Environment.IsDevelopment())
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BDContext>();
            var userManager = services.GetRequiredService<UserManager<GOPUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // Asegurar que la base de datos está creada
            await context.Database.EnsureCreatedAsync();

            // Ejecutar seeding
            await DataSeeder.SeedDataAsync(context, userManager, roleManager, app.Configuration);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error durante el seeding: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        // No re-lanzar la excepción, solo loguear para que la app pueda continuar
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "GOP API";
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
#endregion

#region APP
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseRequestLocalization();
#endregion

app.Run();

static class OpenApiCycleBreaker
{
    // Para cada DTO, las propiedades de navegación a un solo objeto "padre/relacionado" que ya
    // están cubiertas por su respectivo campo *Id y que cierran un ciclo en el grafo de DTOs.
    // Se excluyen sólo del schema de OpenAPI (ver AddOpenApi/Http.Json.JsonOptions en Program.cs),
    // no de la serialización real de los controllers.
    static readonly Dictionary<Type, string[]> PropiedadesCirculares = new()
    {
        [typeof(ContratoDTO)] = ["Zona", "Empresa"],
        [typeof(ContratoDocDTO)] = ["Contrato"],
        [typeof(ContratoItemDTO)] = ["Contrato", "Item"],
        [typeof(ContratoItemControlDTO)] = ["ContratoItem", "ItemControl"],
        [typeof(ContratoItemControlDocDTO)] = ["ContratoItemControl", "ItemControlDoc"],
        [typeof(ContratoItemControlParamDTO)] = ["ContratoItemControl", "ItemControlParam", "Unidad"],
        [typeof(ContratoEstructuraDTO)] = ["Contrato", "Calle", "EstructuraTipo", "EntreCalle", "YCalle", "EsquinaCalle"],
        [typeof(ContratoEstructuraDocDTO)] = ["ContratoEstructura"],
        [typeof(CertificadoDTO)] = ["Contrato", "ZonaProfesional", "EmpresaProfesional"],
        [typeof(CertificadoDocDTO)] = ["Certificado"],
        [typeof(CertificadoItemDTO)] = ["Certificado", "ItemContrato", "FrenteObra", "ContratoEstructura", "Unidad", "CertificadoItemDef"],
        [typeof(CertificadoItemDefDTO)] = ["Certificado", "ItemContrato", "Unidad"],
        [typeof(CertificadoItemControlDTO)] = ["CertificadoItem", "ContratoItemControl"],
        [typeof(CertificadoItemControlDocDTO)] = ["CertificadoItemControl"],
        [typeof(CertificadoItemControlParamDTO)] = ["CertificadoItemControl", "ContratoItemControlParam", "Unidad"],
        [typeof(CertificadoItemControlParamDocDTO)] = ["CertificadoItemControlParam"],
        [typeof(EventoDTO)] = ["Tipo", "Contrato", "Certificado", "Zona", "FrenteObra", "Empresa"],
        [typeof(EventoDocDTO)] = ["Evento"],
        [typeof(EventoParamDTO)] = ["Evento", "Unidad"],
        [typeof(EventoParamDocDTO)] = ["EventoParam"],
        [typeof(EventoRelacionadoDTO)] = ["Evento", "EventoRelacionado"],
        [typeof(EmpresaProfesionalDTO)] = ["Empresa", "Persona"],
        [typeof(ZonaProfesionalDTO)] = ["Zona", "Persona"],
        [typeof(FrenteObraDTO)] = ["Zona"],
        [typeof(FrenteObraProfesionalDTO)] = ["FrenteObra", "Persona"],
        [typeof(ItemDTO)] = ["Unidad"],
        [typeof(ItemDocDTO)] = ["Item"],
        [typeof(ItemControlDTO)] = ["Item"],
        [typeof(ItemControlDocDTO)] = ["ItemControl"],
        [typeof(ItemControlParamDTO)] = ["ItemControl", "Unidad"],
        [typeof(ParamCatalogDTO)] = ["Unidad"],
    };

    public static void RemoveCyclicNavigationProperties(JsonTypeInfo typeInfo)
    {
        if (!PropiedadesCirculares.TryGetValue(typeInfo.Type, out var nombres))
            return;

        // ShouldSerialize solo afecta la serialización de instancias; el exportador de schema de
        // OpenAPI recorre la metadata del tipo igual. Hay que sacar la propiedad de la lista para
        // que ni siquiera aparezca en el schema generado.
        for (var i = typeInfo.Properties.Count - 1; i >= 0; i--)
        {
            if (Array.IndexOf(nombres, typeInfo.Properties[i].Name) >= 0)
                typeInfo.Properties.RemoveAt(i);
        }
    }
}
