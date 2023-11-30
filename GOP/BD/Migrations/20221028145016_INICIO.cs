using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace GOP.BD.Migrations
{
    public partial class INICIO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UbicacionCentral = table.Column<Point>(type: "geography", nullable: true),
                    UbicacionInicio = table.Column<Point>(type: "geography", nullable: true),
                    UbicacionFin = table.Column<Point>(type: "geography", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUIT = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UbicacionEmpresa = table.Column<Point>(type: "geography", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstructuraTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodTipo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DescTipo = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructuraTipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventoTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodTipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DescTipo = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoTipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodUnidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DescUnidad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoZona = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NombreZona = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UbicacionZona = table.Column<Point>(type: "geography", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpresaProfesionales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    DescFuncProf = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaProfesionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpresaProfesionales_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpresaProfesionales_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodItem = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DescItem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parametro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parametros_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumExpediente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Caratula = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ZonaId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contratos_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FrenteObras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZonaId = table.Column<int>(type: "int", nullable: false),
                    CodFrenteObra = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NombreFrenteObra = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UbicacionFrente = table.Column<Point>(type: "geography", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrenteObras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrenteObras_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZonaProfesionales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZonaId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    DescFuncProf = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonaProfesionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZonaProfesionales_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZonaProfesionales_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemControles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CodControl = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DescControl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemControles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemControles_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDocs_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContratoDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoDocs_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContratoEstructuras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    EstructuraTipoId = table.Column<int>(type: "int", nullable: false),
                    CodEstructura = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    DescEstructura = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Longitud = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Ancho = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    CalleId = table.Column<int>(type: "int", nullable: true),
                    EntreCalleId = table.Column<int>(type: "int", nullable: true),
                    YCalleId = table.Column<int>(type: "int", nullable: true),
                    EsquinaCalleId = table.Column<int>(type: "int", nullable: true),
                    UbicacionCentral = table.Column<Point>(type: "geography", nullable: true),
                    UbicacionInicio = table.Column<Point>(type: "geography", nullable: true),
                    UbicacionFin = table.Column<Point>(type: "geography", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoEstructuras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoEstructuras_Calles_CalleId",
                        column: x => x.CalleId,
                        principalTable: "Calles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContratoEstructuras_Calles_EntreCalleId",
                        column: x => x.EntreCalleId,
                        principalTable: "Calles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContratoEstructuras_Calles_EsquinaCalleId",
                        column: x => x.EsquinaCalleId,
                        principalTable: "Calles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContratoEstructuras_Calles_YCalleId",
                        column: x => x.YCalleId,
                        principalTable: "Calles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContratoEstructuras_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContratoEstructuras_EstructuraTipos_EstructuraTipoId",
                        column: x => x.EstructuraTipoId,
                        principalTable: "EstructuraTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContratoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CodItem = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DescItem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CantidadTotal = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Coeficiente = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoItems_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContratoItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FrenteObraProfesionales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrenteObraId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    DescFuncProf = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrenteObraProfesionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrenteObraProfesionales_FrenteObras_FrenteObraId",
                        column: x => x.FrenteObraId,
                        principalTable: "FrenteObras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FrenteObraProfesionales_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certificados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    FechaCertificado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZonaProfesionalId = table.Column<int>(type: "int", nullable: true),
                    EmpresaProfesionalId = table.Column<int>(type: "int", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificados_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_EmpresaProfesionales_EmpresaProfesionalId",
                        column: x => x.EmpresaProfesionalId,
                        principalTable: "EmpresaProfesionales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Certificados_ZonaProfesionales_ZonaProfesionalId",
                        column: x => x.ZonaProfesionalId,
                        principalTable: "ZonaProfesionales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemControlDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemControlId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemControlDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemControlDocs_ItemControles_ItemControlId",
                        column: x => x.ItemControlId,
                        principalTable: "ItemControles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemControlParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemControlId = table.Column<int>(type: "int", nullable: false),
                    TipoParam = table.Column<int>(type: "int", nullable: false),
                    Parametro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemControlParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemControlParams_ItemControles_ItemControlId",
                        column: x => x.ItemControlId,
                        principalTable: "ItemControles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemControlParams_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContratoEstructuraDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoEstructuraId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoEstructuraDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoEstructuraDocs_ContratoEstructuras_ContratoEstructuraId",
                        column: x => x.ContratoEstructuraId,
                        principalTable: "ContratoEstructuras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContratoItemControles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoItemId = table.Column<int>(type: "int", nullable: false),
                    ItemControlId = table.Column<int>(type: "int", nullable: false),
                    CodControl = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DescControl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoItemControles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoItemControles_ContratoItems_ContratoItemId",
                        column: x => x.ContratoItemId,
                        principalTable: "ContratoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContratoItemControles_ItemControles_ItemControlId",
                        column: x => x.ItemControlId,
                        principalTable: "ItemControles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificadoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoDocs_Certificados_CertificadoId",
                        column: x => x.CertificadoId,
                        principalTable: "Certificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoItemDefs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificadoId = table.Column<int>(type: "int", nullable: false),
                    ItemContratoId = table.Column<int>(type: "int", nullable: false),
                    CodItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    CantidadTotal = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    CantidadTotalEjecutado = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    CantidadInformada = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    CantidadTotalInformada = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    CantidadAcumulada = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Coeficiente = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoItemDefs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoItemDefs_Certificados_CertificadoId",
                        column: x => x.CertificadoId,
                        principalTable: "Certificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoItemDefs_ContratoItems_ItemContratoId",
                        column: x => x.ItemContratoId,
                        principalTable: "ContratoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoItemDefs_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Título = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DescEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventoTipoId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: true),
                    CertificadoId = table.Column<int>(type: "int", nullable: true),
                    ZonaId = table.Column<int>(type: "int", nullable: true),
                    FrenteObraId = table.Column<int>(type: "int", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    UbicacionEvento = table.Column<Point>(type: "geography", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Certificados_CertificadoId",
                        column: x => x.CertificadoId,
                        principalTable: "Certificados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Eventos_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Eventos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Eventos_EventoTipos_EventoTipoId",
                        column: x => x.EventoTipoId,
                        principalTable: "EventoTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eventos_FrenteObras_FrenteObraId",
                        column: x => x.FrenteObraId,
                        principalTable: "FrenteObras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Eventos_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContratoItemControlDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoItemControlId = table.Column<int>(type: "int", nullable: false),
                    ItemControlDocId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoItemControlDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoItemControlDocs_ContratoItemControles_ContratoItemControlId",
                        column: x => x.ContratoItemControlId,
                        principalTable: "ContratoItemControles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContratoItemControlDocs_ItemControlDocs_ItemControlDocId",
                        column: x => x.ItemControlDocId,
                        principalTable: "ItemControlDocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContratoItemControlParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoItemControllId = table.Column<int>(type: "int", nullable: false),
                    ContratoItemControlId = table.Column<int>(type: "int", nullable: true),
                    ItemControlParamId = table.Column<int>(type: "int", nullable: false),
                    TipoParam = table.Column<int>(type: "int", nullable: false),
                    Parametro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    ValorDato = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoItemControlParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoItemControlParams_ContratoItemControles_ContratoItemControlId",
                        column: x => x.ContratoItemControlId,
                        principalTable: "ContratoItemControles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContratoItemControlParams_ItemControlParams_ItemControlParamId",
                        column: x => x.ItemControlParamId,
                        principalTable: "ItemControlParams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContratoItemControlParams_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificadoId = table.Column<int>(type: "int", nullable: false),
                    ItemContratoId = table.Column<int>(type: "int", nullable: false),
                    FrenteObraId = table.Column<int>(type: "int", nullable: false),
                    ContratoEstructuraId = table.Column<int>(type: "int", nullable: true),
                    CodItem = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DescItem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    FechaMedicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ubicacion = table.Column<Point>(type: "geography", nullable: true),
                    CantidadTotal = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    CantidadMedida = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    CantidadInformada = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Coeficiente = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    CertificadoItemDefId = table.Column<int>(type: "int", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoItems_CertificadoItemDefs_CertificadoItemDefId",
                        column: x => x.CertificadoItemDefId,
                        principalTable: "CertificadoItemDefs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CertificadoItems_Certificados_CertificadoId",
                        column: x => x.CertificadoId,
                        principalTable: "Certificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoItems_ContratoEstructuras_ContratoEstructuraId",
                        column: x => x.ContratoEstructuraId,
                        principalTable: "ContratoEstructuras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CertificadoItems_ContratoItems_ItemContratoId",
                        column: x => x.ItemContratoId,
                        principalTable: "ContratoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoItems_FrenteObras_FrenteObraId",
                        column: x => x.FrenteObraId,
                        principalTable: "FrenteObras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoItems_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventoDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoDocs_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventoParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    Parametro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    ValorMedido = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoParams_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoParams_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoItemControles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificadoItemId = table.Column<int>(type: "int", nullable: false),
                    ContratoItemControlId = table.Column<int>(type: "int", nullable: false),
                    DescControl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaControl = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UbicacionControl = table.Column<Point>(type: "geography", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoItemControles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoItemControles_CertificadoItems_CertificadoItemId",
                        column: x => x.CertificadoItemId,
                        principalTable: "CertificadoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoItemControles_ContratoItemControles_ContratoItemControlId",
                        column: x => x.ContratoItemControlId,
                        principalTable: "ContratoItemControles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventoParamDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoParamId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoParamDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoParamDocs_EventoParams_EventoParamId",
                        column: x => x.EventoParamId,
                        principalTable: "EventoParams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoItemControlDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificadoItemControlId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoItemControlDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoItemControlDocs_CertificadoItemControles_CertificadoItemControlId",
                        column: x => x.CertificadoItemControlId,
                        principalTable: "CertificadoItemControles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoItemControlParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificadoItemControlId = table.Column<int>(type: "int", nullable: false),
                    ContratoItemControlParamId = table.Column<int>(type: "int", nullable: false),
                    Parametro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    ValorMedido = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    UbicacionParam = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoItemControlParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoItemControlParams_CertificadoItemControles_CertificadoItemControlId",
                        column: x => x.CertificadoItemControlId,
                        principalTable: "CertificadoItemControles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoItemControlParams_ContratoItemControlParams_ContratoItemControlParamId",
                        column: x => x.ContratoItemControlParamId,
                        principalTable: "ContratoItemControlParams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoItemControlParams_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoItemControlParamDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificadoItemControlParamlId = table.Column<int>(type: "int", nullable: false),
                    CertificadoItemControlParamId = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    NomDoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UbicacionDoc = table.Column<Point>(type: "geography", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoItemControlParamDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoItemControlParamDocs_CertificadoItemControlParams_CertificadoItemControlParamId",
                        column: x => x.CertificadoItemControlParamId,
                        principalTable: "CertificadoItemControlParams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonaId",
                table: "AspNetUsers",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDocs_CertificadoId",
                table: "CertificadoDocs",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemControlDocs_CertificadoItemControlId",
                table: "CertificadoItemControlDocs",
                column: "CertificadoItemControlId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemControles_CertificadoItemId",
                table: "CertificadoItemControles",
                column: "CertificadoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemControles_ContratoItemControlId",
                table: "CertificadoItemControles",
                column: "ContratoItemControlId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemControlParamDocs_CertificadoItemControlParamId",
                table: "CertificadoItemControlParamDocs",
                column: "CertificadoItemControlParamId");

            migrationBuilder.CreateIndex(
                name: "CertificadoItemControlParam_ItemControlId_Parametro_UQ",
                table: "CertificadoItemControlParams",
                columns: new[] { "CertificadoItemControlId", "Parametro" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemControlParams_ContratoItemControlParamId",
                table: "CertificadoItemControlParams",
                column: "ContratoItemControlParamId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemControlParams_UnidadId",
                table: "CertificadoItemControlParams",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemDefs_CertificadoId",
                table: "CertificadoItemDefs",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemDefs_ItemContratoId",
                table: "CertificadoItemDefs",
                column: "ItemContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemDefs_UnidadId",
                table: "CertificadoItemDefs",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItems_CertificadoId",
                table: "CertificadoItems",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItems_CertificadoItemDefId",
                table: "CertificadoItems",
                column: "CertificadoItemDefId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItems_ContratoEstructuraId",
                table: "CertificadoItems",
                column: "ContratoEstructuraId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItems_FrenteObraId",
                table: "CertificadoItems",
                column: "FrenteObraId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItems_ItemContratoId",
                table: "CertificadoItems",
                column: "ItemContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItems_UnidadId",
                table: "CertificadoItems",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "Certificado_Periodo_UQ",
                table: "Certificados",
                columns: new[] { "ContratoId", "Periodo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_EmpresaProfesionalId",
                table: "Certificados",
                column: "EmpresaProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_ZonaProfesionalId",
                table: "Certificados",
                column: "ZonaProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoDocs_ContratoId",
                table: "ContratoDocs",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoEstructuraDocs_ContratoEstructuraId",
                table: "ContratoEstructuraDocs",
                column: "ContratoEstructuraId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoEstructuras_CalleId",
                table: "ContratoEstructuras",
                column: "CalleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoEstructuras_ContratoId",
                table: "ContratoEstructuras",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoEstructuras_EntreCalleId",
                table: "ContratoEstructuras",
                column: "EntreCalleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoEstructuras_EsquinaCalleId",
                table: "ContratoEstructuras",
                column: "EsquinaCalleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoEstructuras_EstructuraTipoId",
                table: "ContratoEstructuras",
                column: "EstructuraTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoEstructuras_YCalleId",
                table: "ContratoEstructuras",
                column: "YCalleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoItemControlDocs_ContratoItemControlId",
                table: "ContratoItemControlDocs",
                column: "ContratoItemControlId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoItemControlDocs_ItemControlDocId",
                table: "ContratoItemControlDocs",
                column: "ItemControlDocId");

            migrationBuilder.CreateIndex(
                name: "ContratoItemControl_ContratoItem_CodControl_UQ",
                table: "ContratoItemControles",
                columns: new[] { "ContratoItemId", "CodControl" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ContratoItemControl_ContratoItem_ItemControl_UQ",
                table: "ContratoItemControles",
                columns: new[] { "ContratoItemId", "ItemControlId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContratoItemControles_ItemControlId",
                table: "ContratoItemControles",
                column: "ItemControlId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoItemControlParams_ContratoItemControlId",
                table: "ContratoItemControlParams",
                column: "ContratoItemControlId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoItemControlParams_ItemControlParamId",
                table: "ContratoItemControlParams",
                column: "ItemControlParamId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoItemControlParams_UnidadId",
                table: "ContratoItemControlParams",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "ContratoItem_Contrato_Item_CodItem_UQ",
                table: "ContratoItems",
                columns: new[] { "ContratoId", "ItemId", "CodItem" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContratoItems_ItemId",
                table: "ContratoItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "Contrato_UQ",
                table: "Contratos",
                column: "NumExpediente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EmpresaId",
                table: "Contratos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_ZonaId",
                table: "Contratos",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "EmpresaProfesional_Empresa_Persona_FuncionProfesional_UQ",
                table: "EmpresaProfesionales",
                columns: new[] { "EmpresaId", "PersonaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaProfesionales_PersonaId",
                table: "EmpresaProfesionales",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "Empresal_CUIT_UQ",
                table: "Empresas",
                column: "CUIT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EstructuraTipo_CodTipo_UQ",
                table: "EstructuraTipos",
                column: "CodTipo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventoDocs_EventoId",
                table: "EventoDocs",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoParamDocs_EventoParamId",
                table: "EventoParamDocs",
                column: "EventoParamId");

            migrationBuilder.CreateIndex(
                name: "EventoParam_EventoId_Parametro_UQ",
                table: "EventoParams",
                columns: new[] { "EventoId", "Parametro" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventoParams_UnidadId",
                table: "EventoParams",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "Certificado__UQ",
                table: "Eventos",
                columns: new[] { "ContratoId", "Numero" },
                unique: true,
                filter: "[ContratoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CertificadoId",
                table: "Eventos",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_EmpresaId",
                table: "Eventos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_EventoTipoId",
                table: "Eventos",
                column: "EventoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_FrenteObraId",
                table: "Eventos",
                column: "FrenteObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ZonaId",
                table: "Eventos",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "EventoTipo_CodTipo_UQ",
                table: "EventoTipos",
                column: "CodTipo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FrenteObraProfesional_FrenteObra_Persona_FuncionProfesional_UQ",
                table: "FrenteObraProfesionales",
                columns: new[] { "FrenteObraId", "PersonaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FrenteObraProfesionales_PersonaId",
                table: "FrenteObraProfesionales",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "FrenteObra_Zona_CodFrenteObra_UQ",
                table: "FrenteObras",
                columns: new[] { "ZonaId", "CodFrenteObra" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemControlDocs_ItemControlId",
                table: "ItemControlDocs",
                column: "ItemControlId");

            migrationBuilder.CreateIndex(
                name: "ItemControl_CodControl_UQ",
                table: "ItemControles",
                columns: new[] { "ItemId", "CodControl" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ItemControlParam_ItemControlId_Parametro_UQ",
                table: "ItemControlParams",
                columns: new[] { "ItemControlId", "Parametro" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemControlParams_UnidadId",
                table: "ItemControlParams",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDocs_ItemId",
                table: "ItemDocs",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "Item_CodItem_UQ",
                table: "Items",
                column: "CodItem",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnidadId",
                table: "Items",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Parametros_UnidadId",
                table: "Parametros",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "ParamCatalog_Parametro_UQ",
                table: "Parametros",
                column: "Parametro",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PersonaDNI_UQ",
                table: "Personas",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unidad_CodUnidad_UQ",
                table: "Unidades",
                column: "CodUnidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZonaProfesionales_PersonaId",
                table: "ZonaProfesionales",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "ZonaProfesional_Zona_Persona_FuncionProfesional_UQ",
                table: "ZonaProfesionales",
                columns: new[] { "ZonaId", "PersonaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Zona_CodigoZona_UQ",
                table: "Zonas",
                column: "CodigoZona",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CertificadoDocs");

            migrationBuilder.DropTable(
                name: "CertificadoItemControlDocs");

            migrationBuilder.DropTable(
                name: "CertificadoItemControlParamDocs");

            migrationBuilder.DropTable(
                name: "ContratoDocs");

            migrationBuilder.DropTable(
                name: "ContratoEstructuraDocs");

            migrationBuilder.DropTable(
                name: "ContratoItemControlDocs");

            migrationBuilder.DropTable(
                name: "EventoDocs");

            migrationBuilder.DropTable(
                name: "EventoParamDocs");

            migrationBuilder.DropTable(
                name: "FrenteObraProfesionales");

            migrationBuilder.DropTable(
                name: "ItemDocs");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CertificadoItemControlParams");

            migrationBuilder.DropTable(
                name: "ItemControlDocs");

            migrationBuilder.DropTable(
                name: "EventoParams");

            migrationBuilder.DropTable(
                name: "CertificadoItemControles");

            migrationBuilder.DropTable(
                name: "ContratoItemControlParams");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "CertificadoItems");

            migrationBuilder.DropTable(
                name: "ContratoItemControles");

            migrationBuilder.DropTable(
                name: "ItemControlParams");

            migrationBuilder.DropTable(
                name: "EventoTipos");

            migrationBuilder.DropTable(
                name: "CertificadoItemDefs");

            migrationBuilder.DropTable(
                name: "ContratoEstructuras");

            migrationBuilder.DropTable(
                name: "FrenteObras");

            migrationBuilder.DropTable(
                name: "ItemControles");

            migrationBuilder.DropTable(
                name: "Certificados");

            migrationBuilder.DropTable(
                name: "ContratoItems");

            migrationBuilder.DropTable(
                name: "Calles");

            migrationBuilder.DropTable(
                name: "EstructuraTipos");

            migrationBuilder.DropTable(
                name: "EmpresaProfesionales");

            migrationBuilder.DropTable(
                name: "ZonaProfesionales");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
