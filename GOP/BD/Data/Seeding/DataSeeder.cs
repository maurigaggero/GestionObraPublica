using GOP.BD.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Seeding
{
    // Datos del usuario admin de prueba que crea el seeding. Se leen de configuración
    // (User Secrets en Development) en vez de hardcodearlos, para no exponer datos
    // personales reales en el repo. Ver sección "AdminSeed" en appsettings / user-secrets.
    public class AdminSeedOptions
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool EstaCompleto()
            => !string.IsNullOrWhiteSpace(DNI)
            && !string.IsNullOrWhiteSpace(Nombre)
            && !string.IsNullOrWhiteSpace(Apellido)
            && !string.IsNullOrWhiteSpace(Email)
            && !string.IsNullOrWhiteSpace(UserName)
            && !string.IsNullOrWhiteSpace(Password);
    }

    public class DataSeeder
    {
        public static async Task SeedDataAsync(BDContext context, UserManager<GOPUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            try
            {
                // Crear roles si no existen
                await SeedRolesAsync(roleManager);

                // Crear datos base que no dependen de otros
                await SeedUnidadesAsync(context);
                await SeedEstructuraTiposAsync(context);
                await SeedCallesAsync(context);
                await SeedParamCatalogAsync(context);
                await SeedEventoTiposAsync(context);

                // Crear datos que dependen de otros
                await SeedZonasAsync(context);
                await SeedEmpresasAsync(context);

                // Crear Persona y Usuario admin de prueba (opcional, requiere configurar
                // AdminSeed en User Secrets: dotnet user-secrets set "AdminSeed:Email" "...")
                var adminSeed = configuration.GetSection("AdminSeed").Get<AdminSeedOptions>() ?? new AdminSeedOptions();
                if (adminSeed.EstaCompleto())
                {
                    await SeedPersonaAsync(context, adminSeed);
                    await SeedUserAsync(context, userManager, adminSeed);
                }
                else
                {
                    Console.WriteLine("⚠ AdminSeed no está configurado (User Secrets); se omite la creación del usuario admin de prueba.");
                }

                Console.WriteLine("✓ Seeding completado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error en seeding: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Usuario", "Consultor", "BaseDatos", "HyS", "Zona1", "Zona2", "Frente", "Consulta1", "Consulta2" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedUnidadesAsync(BDContext context)
        {
            // Verificar si ya existen unidades
            if (await context.Unidades.AnyAsync())
                return;

            var unidades = new List<Unidad>
            {
                new Unidad { CodUnidad = "MTS", DescUnidad = "Metro", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "MTS2", DescUnidad = "Metro Cuadrado", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "MTS3", DescUnidad = "Metro Cúbico", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "KM", DescUnidad = "Kilómetro", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "LT", DescUnidad = "Litro", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "KG", DescUnidad = "Kilogramo", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "TON", DescUnidad = "Tonelada", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "UN", DescUnidad = "Unidad", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "DIA", DescUnidad = "Día", EstadoRegistro = 0 },
                new Unidad { CodUnidad = "HRS", DescUnidad = "Hora", EstadoRegistro = 0 }
            };

            context.Unidades.AddRange(unidades);
            await context.SaveChangesAsync();
        }

        private static async Task SeedEstructuraTiposAsync(BDContext context)
        {
            // Verificar si ya existen estructuras de tipo
            if (await context.EstructuraTipos.AnyAsync())
                return;

            var estructuras = new List<EstructuraTipo>
            {
                new EstructuraTipo { CodTipo = "PA", DescTipo = "Pavimento rigido de Hormigón", EstadoRegistro = 0 },
                new EstructuraTipo { CodTipo = "HM", DescTipo = "Hormigón", EstadoRegistro = 0 },
                new EstructuraTipo { CodTipo = "AS", DescTipo = "Asfalto", EstadoRegistro = 0 },
                new EstructuraTipo { CodTipo = "AD", DescTipo = "Adoquín", EstadoRegistro = 0 },
                new EstructuraTipo { CodTipo = "RP", DescTipo = "Ripio", EstadoRegistro = 0 }
            };

            context.EstructuraTipos.AddRange(estructuras);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCallesAsync(BDContext context)
        {
            // Verificar si ya existen calles
            if (await context.Calles.AnyAsync())
                return;

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var calles = new List<Calle>
            {
                new Calle
                {
                    NombreCalle = "Avenida Argentina",
                    UbicacionInicio = geometryFactory.CreatePoint(new Coordinate(-68.13, -38.75)),
                    UbicacionCentral = geometryFactory.CreatePoint(new Coordinate(-68.13, -38.755)),
                    UbicacionFin = geometryFactory.CreatePoint(new Coordinate(-68.13, -38.76)),
                    EstadoRegistro = 0
                },
                new Calle
                {
                    NombreCalle = "Calle San Juan",
                    UbicacionInicio = geometryFactory.CreatePoint(new Coordinate(-68.12, -38.74)),
                    UbicacionCentral = geometryFactory.CreatePoint(new Coordinate(-68.125, -38.745)),
                    UbicacionFin = geometryFactory.CreatePoint(new Coordinate(-68.13, -38.75)),
                    EstadoRegistro = 0
                },
                new Calle
                {
                    NombreCalle = "Avenida Diagonal",
                    UbicacionInicio = geometryFactory.CreatePoint(new Coordinate(-68.14, -38.80)),
                    UbicacionCentral = geometryFactory.CreatePoint(new Coordinate(-68.135, -38.775)),
                    UbicacionFin = geometryFactory.CreatePoint(new Coordinate(-68.13, -38.75)),
                    EstadoRegistro = 0
                }
            };

            context.Calles.AddRange(calles);
            await context.SaveChangesAsync();
        }

        private static async Task SeedParamCatalogAsync(BDContext context)
        {
            // Verificar si ya existen parámetros
            if (await context.Parametros.AnyAsync())
                return;

            // Obtener la primera unidad para usar como referencia
            var unidad = await context.Unidades.FirstOrDefaultAsync();
            if (unidad == null)
                return; // No hay unidades, no se puede crear parámetros

            var parametros = new List<ParamCatalog>
            {
                new ParamCatalog
                {
                    Parametro = "TEMP",
                    Descripción = "Temperatura ambiente",
                    UnidadId = unidad.Id,
                    ValorMinimo = -40,
                    ValorMaximo = 50,
                    EstadoRegistro = 0
                },
                new ParamCatalog
                {
                    Parametro = "HUME",
                    Descripción = "Humedad relativa",
                    UnidadId = unidad.Id,
                    ValorMinimo = 0,
                    ValorMaximo = 100,
                    EstadoRegistro = 0
                },
                new ParamCatalog
                {
                    Parametro = "PREC",
                    Descripción = "Precipitación",
                    UnidadId = unidad.Id,
                    ValorMinimo = 0,
                    ValorMaximo = 500,
                    EstadoRegistro = 0
                },
                new ParamCatalog
                {
                    Parametro = "VIEN",
                    Descripción = "Velocidad del viento",
                    UnidadId = unidad.Id,
                    ValorMinimo = 0,
                    ValorMaximo = 100,
                    EstadoRegistro = 0
                },
                new ParamCatalog
                {
                    Parametro = "COMP",
                    Descripción = "Compacidad del suelo",
                    UnidadId = unidad.Id,
                    ValorMinimo = 0,
                    ValorMaximo = 100,
                    EstadoRegistro = 0
                }
            };

            context.Parametros.AddRange(parametros);
            await context.SaveChangesAsync();
        }

        private static async Task SeedEventoTiposAsync(BDContext context)
        {
            // Verificar si ya existen tipos de eventos
            if (await context.EventoTipos.AnyAsync())
                return;

            var tiposEventos = new List<EventoTipo>
            {
                new EventoTipo
                {
                    CodTipo = "CLIMA",
                    DescTipo = "Eventos climáticos - Lluvia, nieve, viento fuerte, etc.",
                    EstadoRegistro = 0
                },
                new EventoTipo
                {
                    CodTipo = "ACCE",
                    DescTipo = "Accidente o incidente en obra",
                    EstadoRegistro = 0
                },
                new EventoTipo
                {
                    CodTipo = "PARA",
                    DescTipo = "Parada de obra - Suspensión de actividades",
                    EstadoRegistro = 0
                },
                new EventoTipo
                {
                    CodTipo = "HYS",
                    DescTipo = "Evento de Higiene y Seguridad",
                    EstadoRegistro = 0
                },
                new EventoTipo
                {
                    CodTipo = "INSP",
                    DescTipo = "Inspección de obra",
                    EstadoRegistro = 0
                },
                new EventoTipo
                {
                    CodTipo = "RECA",
                    DescTipo = "Recapacitación",
                    EstadoRegistro = 0
                },
                new EventoTipo
                {
                    CodTipo = "OTRO",
                    DescTipo = "Otro tipo de evento",
                    EstadoRegistro = 0
                }
            };

            context.EventoTipos.AddRange(tiposEventos);
            await context.SaveChangesAsync();
        }

        private static async Task SeedZonasAsync(BDContext context)
        {
            // Verificar si ya existen zonas
            if (await context.Zonas.AnyAsync())
                return;

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var zonas = new List<Zona>
            {
                new Zona
                {
                    CodigoZona = "Z001",
                    NombreZona = "Zona Centro",
                    UbicacionZona = geometryFactory.CreatePoint(new Coordinate(-68.13, -38.75)),
                    EstadoRegistro = 0
                },
                new Zona
                {
                    CodigoZona = "Z002",
                    NombreZona = "Zona Norte",
                    UbicacionZona = geometryFactory.CreatePoint(new Coordinate(-68.12, -38.70)),
                    EstadoRegistro = 0
                },
                new Zona
                {
                    CodigoZona = "Z003",
                    NombreZona = "Zona Sur",
                    UbicacionZona = geometryFactory.CreatePoint(new Coordinate(-68.14, -38.80)),
                    EstadoRegistro = 0
                }
            };

            context.Zonas.AddRange(zonas);
            await context.SaveChangesAsync();
        }

        private static async Task SeedEmpresasAsync(BDContext context)
        {
            // Verificar si ya existen empresas
            if (await context.Empresas.AnyAsync())
                return;

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var empresas = new List<Empresa>
            {
                new Empresa
                {
                    Nombre = "Constructora del Sur S.A.",
                    CUIT = "30701234567",
                    UbicacionEmpresa = geometryFactory.CreatePoint(new Coordinate(-68.13, -38.75)),
                    EstadoRegistro = 0
                },
                new Empresa
                {
                    Nombre = "Infraestructuras del Neuquén",
                    CUIT = "30702345678",
                    UbicacionEmpresa = geometryFactory.CreatePoint(new Coordinate(-68.12, -38.70)),
                    EstadoRegistro = 0
                },
                new Empresa
                {
                    Nombre = "Obras Públicas Patagonia",
                    CUIT = "30703456789",
                    UbicacionEmpresa = geometryFactory.CreatePoint(new Coordinate(-68.14, -38.80)),
                    EstadoRegistro = 0
                }
            };

            context.Empresas.AddRange(empresas);
            await context.SaveChangesAsync();
        }

        private static async Task SeedPersonaAsync(BDContext context, AdminSeedOptions adminSeed)
        {
            // Verificar si ya existe una persona con este DNI
            var personaExistente = await context.Personas.FirstOrDefaultAsync(p => p.DNI == adminSeed.DNI);

            if (personaExistente == null)
            {
                var persona = new Persona
                {
                    DNI = adminSeed.DNI,
                    Nombre = adminSeed.Nombre,
                    Apellido = adminSeed.Apellido,
                    Email = adminSeed.Email,
                    Telefono = adminSeed.Telefono,
                    EstadoRegistro = 0
                };

                context.Personas.Add(persona);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedUserAsync(BDContext context, UserManager<GOPUser> userManager, AdminSeedOptions adminSeed)
        {
            // Verificar si ya existe el usuario
            var usuarioExistente = await userManager.FindByNameAsync(adminSeed.UserName);

            if (usuarioExistente == null)
            {
                // Obtener la persona creada
                var persona = await context.Personas.FirstOrDefaultAsync(p => p.DNI == adminSeed.DNI);

                if (persona != null)
                {
                    var nuevoUsuario = new GOPUser
                    {
                        UserName = adminSeed.UserName,
                        Email = adminSeed.Email,
                        EmailConfirmed = true,
                        PersonaId = persona.Id
                    };

                    // Crear el usuario con contraseña
                    var resultado = await userManager.CreateAsync(nuevoUsuario, adminSeed.Password);

                    if (resultado.Succeeded)
                    {
                        // Asignar rol Admin
                        await userManager.AddToRoleAsync(nuevoUsuario, "Admin");

                        // Agregar claims (incluye el claim de rol que espera [Authorize(Roles = ...)],
                        // ya que ConstruirToken arma el JWT a partir de userManager.GetClaimsAsync,
                        // no de los roles de AddToRoleAsync)
                        var claims = new[]
                        {
                            new System.Security.Claims.Claim("Email", nuevoUsuario.Email),
                            new System.Security.Claims.Claim("Admin", "true"),
                            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Admin")
                        };

                        foreach (var claim in claims)
                        {
                            await userManager.AddClaimAsync(nuevoUsuario, claim);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error al crear usuario: {string.Join(", ", resultado.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }
    }
}
