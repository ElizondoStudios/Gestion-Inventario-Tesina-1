using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ModulosYPerfilesPuesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDPerfilPuesto",
                table: "Usuarios",
                newName: "PerfilPuestoIDPerfilPuesto");

            migrationBuilder.CreateTable(
                name: "PerfilPuesto",
                columns: table => new
                {
                    IDPerfilPuesto = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilPuesto", x => x.IDPerfilPuesto);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios",
                column: "PerfilPuestoIDPerfilPuesto");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_PerfilPuesto_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios",
                column: "PerfilPuestoIDPerfilPuesto",
                principalTable: "PerfilPuesto",
                principalColumn: "IDPerfilPuesto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_PerfilPuesto_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "PerfilPuesto");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "PerfilPuestoIDPerfilPuesto",
                table: "Usuarios",
                newName: "IDPerfilPuesto");
        }
    }
}
