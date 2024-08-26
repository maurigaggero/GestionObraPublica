using AutoMapper;
using EFCorePeliculas.Servicios;
using GOP.BD.Data;
using GOP.Repositorio;
using GOP.Repositorio.Repos;
using GOP.Server.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GOP", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    }
    );

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                  {
                                                      {
                                                          new OpenApiSecurityScheme
                                                          {
                                                              Reference = new OpenApiReference
                                                              {
                                                                  Type = ReferenceType.SecurityScheme,
                                                                  Id = "Bearer"
                                                              }
                                                          },
                                                          new string[]{}
                                                      }
                                                  }
    );
}
);

#region INJECTION SERVICES      
builder.Services.AddScoped<IRepositorio<IEntidadBase>, Repositorio<IEntidadBase>>();
builder.Services.AddScoped<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
builder.Services.AddSingleton(provider =>
    new MapperConfiguration(config =>
    {
        var geometryFactory = provider.GetRequiredService<GeometryFactory>();
        config.AddProfile(new AutoMapperProfiles(geometryFactory));
    }).CreateMapper()
);


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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
#endregion

#region APP
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GOP v1"));

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
