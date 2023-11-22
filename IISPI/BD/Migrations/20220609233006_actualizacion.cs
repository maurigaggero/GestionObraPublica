using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IISPI.BD.Migrations
{
    public partial class actualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "CertificadoItemControl_CertificadoItem_CodControl_UQ",
                table: "CertificadoItemControles");

            migrationBuilder.DropIndex(
                name: "CertificadoItemControl_CertificadoItem_ContratoItemControl_UQ",
                table: "CertificadoItemControles");

            migrationBuilder.AddColumn<decimal>(
                name: "Coeficiente",
                table: "ContratoItems",
                type: "Decimal(10,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CantidadInformada",
                table: "CertificadoItems",
                type: "Decimal(10,3)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.CreateIndex(
                name: "CertificadoItemControl_CertificadoItem_CodControl_UQ",
                table: "CertificadoItemControles",
                columns: new[] { "CertificadoItemId", "ContratoItemControlId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parametros_UnidadId",
                table: "Parametros",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "ParamCatalog_Parametro_UQ",
                table: "Parametros",
                column: "Parametro",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropIndex(
                name: "CertificadoItemControl_CertificadoItem_CodControl_UQ",
                table: "CertificadoItemControles");

            migrationBuilder.DropColumn(
                name: "Coeficiente",
                table: "ContratoItems");

            migrationBuilder.DropColumn(
                name: "CantidadInformada",
                table: "CertificadoItems");

            migrationBuilder.CreateIndex(
                name: "CertificadoItemControl_CertificadoItem_CodControl_UQ",
                table: "CertificadoItemControles",
                columns: new[] { "CertificadoItemId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CertificadoItemControl_CertificadoItem_ContratoItemControl_UQ",
                table: "CertificadoItemControles",
                columns: new[] { "CertificadoItemId", "ContratoItemControlId" },
                unique: true);
        }
    }
}
