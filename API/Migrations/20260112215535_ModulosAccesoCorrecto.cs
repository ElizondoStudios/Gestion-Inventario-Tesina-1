using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ModulosAccesoCorrecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModulosCategorias",
                columns: table => new
                {
                    IDModuloCategoria = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Icono = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulosCategorias", x => x.IDModuloCategoria);
                });

            migrationBuilder.CreateTable(
                name: "NivelesAcceso",
                columns: table => new
                {
                    NivelAcceso = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesAcceso", x => x.NivelAcceso);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    IDModulo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Icono = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModuloCategoriaIDModuloCategoria = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.IDModulo);
                    table.ForeignKey(
                        name: "FK_Modulos_ModulosCategorias_ModuloCategoriaIDModuloCategoria",
                        column: x => x.ModuloCategoriaIDModuloCategoria,
                        principalTable: "ModulosCategorias",
                        principalColumn: "IDModuloCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModulosAcceso",
                columns: table => new
                {
                    IDModuloAcceso = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModuloIDModulo = table.Column<int>(type: "INTEGER", nullable: false),
                    NivelAcceso1 = table.Column<int>(type: "INTEGER", nullable: false),
                    PerfilPuestoIDPerfilPuesto = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulosAcceso", x => x.IDModuloAcceso);
                    table.ForeignKey(
                        name: "FK_ModulosAcceso_Modulos_ModuloIDModulo",
                        column: x => x.ModuloIDModulo,
                        principalTable: "Modulos",
                        principalColumn: "IDModulo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModulosAcceso_NivelesAcceso_NivelAcceso1",
                        column: x => x.NivelAcceso1,
                        principalTable: "NivelesAcceso",
                        principalColumn: "NivelAcceso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModulosAcceso_PerfilPuesto_PerfilPuestoIDPerfilPuesto",
                        column: x => x.PerfilPuestoIDPerfilPuesto,
                        principalTable: "PerfilPuesto",
                        principalColumn: "IDPerfilPuesto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_ModuloCategoriaIDModuloCategoria",
                table: "Modulos",
                column: "ModuloCategoriaIDModuloCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ModulosAcceso_ModuloIDModulo",
                table: "ModulosAcceso",
                column: "ModuloIDModulo");

            migrationBuilder.CreateIndex(
                name: "IX_ModulosAcceso_NivelAcceso1",
                table: "ModulosAcceso",
                column: "NivelAcceso1");

            migrationBuilder.CreateIndex(
                name: "IX_ModulosAcceso_PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso",
                column: "PerfilPuestoIDPerfilPuesto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModulosAcceso");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "NivelesAcceso");

            migrationBuilder.DropTable(
                name: "ModulosCategorias");
        }
    }
}
