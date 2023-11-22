using IISPI.BD.Data;
using IISPI.Repositorio;
using IISPI.Repositorio.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

#region SERVICES
var conn = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<BDContext>(opciones => opciones.UseSqlServer(conn));

builder.Services.AddIdentity<IISPIUser, IdentityRole>()
                .AddEntityFrameworkStores<BDContext>()
                .AddDefaultTokenProviders();

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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IISPI", Version = "v1" });

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
//persona
builder.Services.AddScoped<IPersonasRepositorio, PersonasRepositorio>();
//MODULO ITEM
builder.Services.AddScoped<IUnidadesRepositorio, UnidadesRespositorio>();
builder.Services.AddScoped<IItemsRepositorio, ItemsRepositorio>();
builder.Services.AddScoped<IItemsControlsRepositorio, ItemsControlsRepositorio>();
builder.Services.AddScoped<IItemsControlsParamsRepositorio, ItemsControlsParamsRepositorio>();
builder.Services.AddScoped<IItemsControlsDocsRepositorio, ItemsControlsDocsRepositorio>();



#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("Admin", politica => politica.RequireClaim("Admin"));
    opciones.AddPolicy("Central", politica => politica.RequireClaim("Central"));
    opciones.AddPolicy("Obra", politica => politica.RequireClaim("Obra"));
    opciones.AddPolicy("Consulta", politica => politica.RequireClaim("Consulta"));
    opciones.AddPolicy("Autoridad", politica => politica.RequireClaim("Autoridad"));
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
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IISPI v1"));

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
#endregion

app.Run();

