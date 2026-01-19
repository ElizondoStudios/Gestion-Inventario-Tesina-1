using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DBContextCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Unidad_UnidadIDUnidad",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_Inventario_ProductoNoParte",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_Sucursal_SucursalIDSucursal",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulo_ModulosCategoria_ModuloCategoriaIDModuloCategoria",
                table: "Modulo");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_Modulo_ModuloIDModulo",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_PerfilPuesto_PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_SucursalesInventario_Sucursal_SucursalIDSucursal",
                table: "SucursalesInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_PerfilPuesto_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioSucursal_Sucursal_SucursalIDSucursal",
                table: "UsuarioSucursal");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioSucursal_Usuarios_UsuarioIDUsuario",
                table: "UsuarioSucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioSucursal",
                table: "UsuarioSucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unidad",
                table: "Unidad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sucursal",
                table: "Sucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerfilPuesto",
                table: "PerfilPuesto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModulosCategoria",
                table: "ModulosCategoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modulo",
                table: "Modulo");

            migrationBuilder.RenameTable(
                name: "UsuarioSucursal",
                newName: "UsuariosSucursales");

            migrationBuilder.RenameTable(
                name: "Unidad",
                newName: "Unidades");

            migrationBuilder.RenameTable(
                name: "Sucursal",
                newName: "Sucursales");

            migrationBuilder.RenameTable(
                name: "PerfilPuesto",
                newName: "PerfilesPuesto");

            migrationBuilder.RenameTable(
                name: "ModulosCategoria",
                newName: "ModulosCategorias");

            migrationBuilder.RenameTable(
                name: "Modulo",
                newName: "Modulos");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioSucursal_UsuarioIDUsuario",
                table: "UsuariosSucursales",
                newName: "IX_UsuariosSucursales_UsuarioIDUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioSucursal_SucursalIDSucursal",
                table: "UsuariosSucursales",
                newName: "IX_UsuariosSucursales_SucursalIDSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_Modulo_ModuloCategoriaIDModuloCategoria",
                table: "Modulos",
                newName: "IX_Modulos_ModuloCategoriaIDModuloCategoria");

            migrationBuilder.AlterColumn<string>(
                name: "ProductoNoParte",
                table: "LogInventario",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosSucursales",
                table: "UsuariosSucursales",
                column: "IDSucursalUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unidades",
                table: "Unidades",
                column: "IDUnidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sucursales",
                table: "Sucursales",
                column: "IDSucursal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerfilesPuesto",
                table: "PerfilesPuesto",
                column: "IDPerfilPuesto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModulosCategorias",
                table: "ModulosCategorias",
                column: "IDModuloCategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modulos",
                table: "Modulos",
                column: "IDModulo");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Unidades_UnidadIDUnidad",
                table: "Inventario",
                column: "UnidadIDUnidad",
                principalTable: "Unidades",
                principalColumn: "IDUnidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_Inventario_ProductoNoParte",
                table: "LogInventario",
                column: "ProductoNoParte",
                principalTable: "Inventario",
                principalColumn: "NoParte");

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_Sucursales_SucursalIDSucursal",
                table: "LogInventario",
                column: "SucursalIDSucursal",
                principalTable: "Sucursales",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_ModulosCategorias_ModuloCategoriaIDModuloCategoria",
                table: "Modulos",
                column: "ModuloCategoriaIDModuloCategoria",
                principalTable: "ModulosCategorias",
                principalColumn: "IDModuloCategoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_Modulos_ModuloIDModulo",
                table: "ModulosAcceso",
                column: "ModuloIDModulo",
                principalTable: "Modulos",
                principalColumn: "IDModulo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_PerfilesPuesto_PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso",
                column: "PerfilPuestoIDPerfilPuesto",
                principalTable: "PerfilesPuesto",
                principalColumn: "IDPerfilPuesto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SucursalesInventario_Sucursales_SucursalIDSucursal",
                table: "SucursalesInventario",
                column: "SucursalIDSucursal",
                principalTable: "Sucursales",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_PerfilesPuesto_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios",
                column: "PerfilPuestoIDPerfilPuesto",
                principalTable: "PerfilesPuesto",
                principalColumn: "IDPerfilPuesto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosSucursales_Sucursales_SucursalIDSucursal",
                table: "UsuariosSucursales",
                column: "SucursalIDSucursal",
                principalTable: "Sucursales",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosSucursales_Usuarios_UsuarioIDUsuario",
                table: "UsuariosSucursales",
                column: "UsuarioIDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Unidades_UnidadIDUnidad",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_Inventario_ProductoNoParte",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_Sucursales_SucursalIDSucursal",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_ModulosCategorias_ModuloCategoriaIDModuloCategoria",
                table: "Modulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_Modulos_ModuloIDModulo",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_PerfilesPuesto_PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_SucursalesInventario_Sucursales_SucursalIDSucursal",
                table: "SucursalesInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_PerfilesPuesto_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosSucursales_Sucursales_SucursalIDSucursal",
                table: "UsuariosSucursales");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosSucursales_Usuarios_UsuarioIDUsuario",
                table: "UsuariosSucursales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosSucursales",
                table: "UsuariosSucursales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unidades",
                table: "Unidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sucursales",
                table: "Sucursales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerfilesPuesto",
                table: "PerfilesPuesto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModulosCategorias",
                table: "ModulosCategorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modulos",
                table: "Modulos");

            migrationBuilder.RenameTable(
                name: "UsuariosSucursales",
                newName: "UsuarioSucursal");

            migrationBuilder.RenameTable(
                name: "Unidades",
                newName: "Unidad");

            migrationBuilder.RenameTable(
                name: "Sucursales",
                newName: "Sucursal");

            migrationBuilder.RenameTable(
                name: "PerfilesPuesto",
                newName: "PerfilPuesto");

            migrationBuilder.RenameTable(
                name: "ModulosCategorias",
                newName: "ModulosCategoria");

            migrationBuilder.RenameTable(
                name: "Modulos",
                newName: "Modulo");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosSucursales_UsuarioIDUsuario",
                table: "UsuarioSucursal",
                newName: "IX_UsuarioSucursal_UsuarioIDUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosSucursales_SucursalIDSucursal",
                table: "UsuarioSucursal",
                newName: "IX_UsuarioSucursal_SucursalIDSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_Modulos_ModuloCategoriaIDModuloCategoria",
                table: "Modulo",
                newName: "IX_Modulo_ModuloCategoriaIDModuloCategoria");

            migrationBuilder.AlterColumn<string>(
                name: "ProductoNoParte",
                table: "LogInventario",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioSucursal",
                table: "UsuarioSucursal",
                column: "IDSucursalUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unidad",
                table: "Unidad",
                column: "IDUnidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sucursal",
                table: "Sucursal",
                column: "IDSucursal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerfilPuesto",
                table: "PerfilPuesto",
                column: "IDPerfilPuesto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModulosCategoria",
                table: "ModulosCategoria",
                column: "IDModuloCategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modulo",
                table: "Modulo",
                column: "IDModulo");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Unidad_UnidadIDUnidad",
                table: "Inventario",
                column: "UnidadIDUnidad",
                principalTable: "Unidad",
                principalColumn: "IDUnidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_Inventario_ProductoNoParte",
                table: "LogInventario",
                column: "ProductoNoParte",
                principalTable: "Inventario",
                principalColumn: "NoParte",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_Sucursal_SucursalIDSucursal",
                table: "LogInventario",
                column: "SucursalIDSucursal",
                principalTable: "Sucursal",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulo_ModulosCategoria_ModuloCategoriaIDModuloCategoria",
                table: "Modulo",
                column: "ModuloCategoriaIDModuloCategoria",
                principalTable: "ModulosCategoria",
                principalColumn: "IDModuloCategoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_Modulo_ModuloIDModulo",
                table: "ModulosAcceso",
                column: "ModuloIDModulo",
                principalTable: "Modulo",
                principalColumn: "IDModulo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_PerfilPuesto_PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso",
                column: "PerfilPuestoIDPerfilPuesto",
                principalTable: "PerfilPuesto",
                principalColumn: "IDPerfilPuesto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SucursalesInventario_Sucursal_SucursalIDSucursal",
                table: "SucursalesInventario",
                column: "SucursalIDSucursal",
                principalTable: "Sucursal",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_PerfilPuesto_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios",
                column: "PerfilPuestoIDPerfilPuesto",
                principalTable: "PerfilPuesto",
                principalColumn: "IDPerfilPuesto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioSucursal_Sucursal_SucursalIDSucursal",
                table: "UsuarioSucursal",
                column: "SucursalIDSucursal",
                principalTable: "Sucursal",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioSucursal_Usuarios_UsuarioIDUsuario",
                table: "UsuarioSucursal",
                column: "UsuarioIDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
