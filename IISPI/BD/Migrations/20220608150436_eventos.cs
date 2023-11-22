using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IISPI.BD.Migrations
{
    public partial class eventos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certificados_ContratoId",
                table: "Certificados");

            migrationBuilder.DropIndex(
                name: "CertificadoItemControl_CertificadoItem_CodControl_UQ",
                table: "CertificadoItemControles");

            migrationBuilder.DropColumn(
                name: "CodControl",
                table: "CertificadoItemControles");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Certificados",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "CertificadoItemControles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "CertificadoItemControles",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventoTipo",
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
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Título = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DescEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventoTipoId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    CertificadoId = table.Column<int>(type: "int", nullable: false),
                    ZonaId = table.Column<int>(type: "int", nullable: false),
                    FrenteObraId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evento_Certificados_CertificadoId",
                        column: x => x.CertificadoId,
                        principalTable: "Certificados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_EventoTipo_EventoTipoId",
                        column: x => x.EventoTipoId,
                        principalTable: "EventoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_FrenteObras_FrenteObraId",
                        column: x => x.FrenteObraId,
                        principalTable: "FrenteObras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventoDoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventolId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathDoc = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoDoc_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventoParam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    Parametro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    ValorMedido = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "Decimal(10,3)", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCrea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCrea = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoParam_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoParam_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Certificado__UQ",
                table: "Certificados",
                columns: new[] { "ContratoId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CertificadoItemControl_CertificadoItem_CodControl_UQ",
                table: "CertificadoItemControles",
                columns: new[] { "CertificadoItemId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Certificado__UQ1",
                table: "Evento",
                columns: new[] { "ContratoId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evento_CertificadoId",
                table: "Evento",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_EmpresaId",
                table: "Evento",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_EventoTipoId",
                table: "Evento",
                column: "EventoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_FrenteObraId",
                table: "Evento",
                column: "FrenteObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_ZonaId",
                table: "Evento",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoDoc_EventoId",
                table: "EventoDoc",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "EventoParam_EventoId_Parametro_UQ",
                table: "EventoParam",
                columns: new[] { "EventoId", "Parametro" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventoParam_UnidadId",
                table: "EventoParam",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "EventoTipo_CodTipo_UQ",
                table: "EventoTipo",
                column: "CodTipo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventoDoc");

            migrationBuilder.DropTable(
                name: "EventoParam");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "EventoTipo");

            migrationBuilder.DropIndex(
                name: "Certificado__UQ",
                table: "Certificados");

            migrationBuilder.DropIndex(
                name: "CertificadoItemControl_CertificadoItem_CodControl_UQ",
                table: "CertificadoItemControles");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "CertificadoItemControles");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "CertificadoItemControles");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Certificados",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "CodControl",
                table: "CertificadoItemControles",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_ContratoId",
                table: "Certificados",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "CertificadoItemControl_CertificadoItem_CodControl_UQ",
                table: "CertificadoItemControles",
                columns: new[] { "CertificadoItemId", "CodControl" },
                unique: true);
        }
    }
}
