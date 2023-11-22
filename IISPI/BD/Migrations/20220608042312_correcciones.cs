using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IISPI.BD.Migrations
{
    public partial class correcciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoItemControles_CertificadoItemControles_CertificadoItemControlId",
                table: "CertificadoItemControles");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoItemControles_CertificadoItemControlId",
                table: "CertificadoItemControles");

            migrationBuilder.DropColumn(
                name: "CertificadoItemControlId",
                table: "CertificadoItemControles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CertificadoItemControlId",
                table: "CertificadoItemControles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoItemControles_CertificadoItemControlId",
                table: "CertificadoItemControles",
                column: "CertificadoItemControlId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoItemControles_CertificadoItemControles_CertificadoItemControlId",
                table: "CertificadoItemControles",
                column: "CertificadoItemControlId",
                principalTable: "CertificadoItemControles",
                principalColumn: "Id");
        }
    }
}
