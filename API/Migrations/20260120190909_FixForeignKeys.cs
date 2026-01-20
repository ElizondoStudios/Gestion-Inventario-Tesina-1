using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_LogInventario_TiposMovimientosInventario_TipoMovimientoIDTipoMovimientoInventario",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_Usuarios_QuienRealizaIDUsuario",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_ModulosCategorias_ModuloCategoriaIDModuloCategoria",
                table: "Modulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_Modulos_ModuloIDModulo",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_NivelesAcceso_NivelAcceso1",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_PerfilesPuesto_PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_SucursalesInventario_Inventario_ProductoNoParte",
                table: "SucursalesInventario");

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

            migrationBuilder.DropIndex(
                name: "IX_SucursalesInventario_ProductoNoParte",
                table: "SucursalesInventario");

            migrationBuilder.DropIndex(
                name: "IX_LogInventario_ProductoNoParte",
                table: "LogInventario");

            migrationBuilder.DropColumn(
                name: "ProductoNoParte",
                table: "SucursalesInventario");

            migrationBuilder.DropColumn(
                name: "ProductoNoParte",
                table: "LogInventario");

            migrationBuilder.RenameColumn(
                name: "UsuarioIDUsuario",
                table: "UsuariosSucursales",
                newName: "IDUsuario");

            migrationBuilder.RenameColumn(
                name: "SucursalIDSucursal",
                table: "UsuariosSucursales",
                newName: "IDSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosSucursales_UsuarioIDUsuario",
                table: "UsuariosSucursales",
                newName: "IX_UsuariosSucursales_IDUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosSucursales_SucursalIDSucursal",
                table: "UsuariosSucursales",
                newName: "IX_UsuariosSucursales_IDSucursal");

            migrationBuilder.RenameColumn(
                name: "PerfilPuestoIDPerfilPuesto",
                table: "Usuarios",
                newName: "IDPerfilPuesto");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_PerfilPuestoIDPerfilPuesto",
                table: "Usuarios",
                newName: "IX_Usuarios_IDPerfilPuesto");

            migrationBuilder.RenameColumn(
                name: "SucursalIDSucursal",
                table: "SucursalesInventario",
                newName: "IDSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_SucursalesInventario_SucursalIDSucursal",
                table: "SucursalesInventario",
                newName: "IX_SucursalesInventario_IDSucursal");

            migrationBuilder.RenameColumn(
                name: "PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso",
                newName: "IDPerfilPuesto");

            migrationBuilder.RenameColumn(
                name: "NivelAcceso1",
                table: "ModulosAcceso",
                newName: "IDNivelAcceso");

            migrationBuilder.RenameColumn(
                name: "ModuloIDModulo",
                table: "ModulosAcceso",
                newName: "IDModulo");

            migrationBuilder.RenameIndex(
                name: "IX_ModulosAcceso_PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso",
                newName: "IX_ModulosAcceso_IDPerfilPuesto");

            migrationBuilder.RenameIndex(
                name: "IX_ModulosAcceso_NivelAcceso1",
                table: "ModulosAcceso",
                newName: "IX_ModulosAcceso_IDNivelAcceso");

            migrationBuilder.RenameIndex(
                name: "IX_ModulosAcceso_ModuloIDModulo",
                table: "ModulosAcceso",
                newName: "IX_ModulosAcceso_IDModulo");

            migrationBuilder.RenameColumn(
                name: "ModuloCategoriaIDModuloCategoria",
                table: "Modulos",
                newName: "IDModuloCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_Modulos_ModuloCategoriaIDModuloCategoria",
                table: "Modulos",
                newName: "IX_Modulos_IDModuloCategoria");

            migrationBuilder.RenameColumn(
                name: "TipoMovimientoIDTipoMovimientoInventario",
                table: "LogInventario",
                newName: "IDUsuario");

            migrationBuilder.RenameColumn(
                name: "SucursalIDSucursal",
                table: "LogInventario",
                newName: "IDTipoMovimiento");

            migrationBuilder.RenameColumn(
                name: "QuienRealizaIDUsuario",
                table: "LogInventario",
                newName: "IDSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_LogInventario_TipoMovimientoIDTipoMovimientoInventario",
                table: "LogInventario",
                newName: "IX_LogInventario_IDUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_LogInventario_SucursalIDSucursal",
                table: "LogInventario",
                newName: "IX_LogInventario_IDTipoMovimiento");

            migrationBuilder.RenameIndex(
                name: "IX_LogInventario_QuienRealizaIDUsuario",
                table: "LogInventario",
                newName: "IX_LogInventario_IDSucursal");

            migrationBuilder.RenameColumn(
                name: "UnidadIDUnidad",
                table: "Inventario",
                newName: "IDUnidad");

            migrationBuilder.RenameIndex(
                name: "IX_Inventario_UnidadIDUnidad",
                table: "Inventario",
                newName: "IX_Inventario_IDUnidad");

            migrationBuilder.AddColumn<string>(
                name: "NoParte",
                table: "SucursalesInventario",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoParte",
                table: "LogInventario",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalesInventario_NoParte",
                table: "SucursalesInventario",
                column: "NoParte");

            migrationBuilder.CreateIndex(
                name: "IX_LogInventario_NoParte",
                table: "LogInventario",
                column: "NoParte");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Unidades_IDUnidad",
                table: "Inventario",
                column: "IDUnidad",
                principalTable: "Unidades",
                principalColumn: "IDUnidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_Inventario_NoParte",
                table: "LogInventario",
                column: "NoParte",
                principalTable: "Inventario",
                principalColumn: "NoParte",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_Sucursales_IDSucursal",
                table: "LogInventario",
                column: "IDSucursal",
                principalTable: "Sucursales",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_TiposMovimientosInventario_IDTipoMovimiento",
                table: "LogInventario",
                column: "IDTipoMovimiento",
                principalTable: "TiposMovimientosInventario",
                principalColumn: "IDTipoMovimientoInventario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_Usuarios_IDUsuario",
                table: "LogInventario",
                column: "IDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_ModulosCategorias_IDModuloCategoria",
                table: "Modulos",
                column: "IDModuloCategoria",
                principalTable: "ModulosCategorias",
                principalColumn: "IDModuloCategoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_Modulos_IDModulo",
                table: "ModulosAcceso",
                column: "IDModulo",
                principalTable: "Modulos",
                principalColumn: "IDModulo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_NivelesAcceso_IDNivelAcceso",
                table: "ModulosAcceso",
                column: "IDNivelAcceso",
                principalTable: "NivelesAcceso",
                principalColumn: "NivelAcceso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_PerfilesPuesto_IDPerfilPuesto",
                table: "ModulosAcceso",
                column: "IDPerfilPuesto",
                principalTable: "PerfilesPuesto",
                principalColumn: "IDPerfilPuesto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SucursalesInventario_Inventario_NoParte",
                table: "SucursalesInventario",
                column: "NoParte",
                principalTable: "Inventario",
                principalColumn: "NoParte",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SucursalesInventario_Sucursales_IDSucursal",
                table: "SucursalesInventario",
                column: "IDSucursal",
                principalTable: "Sucursales",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_PerfilesPuesto_IDPerfilPuesto",
                table: "Usuarios",
                column: "IDPerfilPuesto",
                principalTable: "PerfilesPuesto",
                principalColumn: "IDPerfilPuesto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosSucursales_Sucursales_IDSucursal",
                table: "UsuariosSucursales",
                column: "IDSucursal",
                principalTable: "Sucursales",
                principalColumn: "IDSucursal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosSucursales_Usuarios_IDUsuario",
                table: "UsuariosSucursales",
                column: "IDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Unidades_IDUnidad",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_Inventario_NoParte",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_Sucursales_IDSucursal",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_TiposMovimientosInventario_IDTipoMovimiento",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_LogInventario_Usuarios_IDUsuario",
                table: "LogInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_ModulosCategorias_IDModuloCategoria",
                table: "Modulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_Modulos_IDModulo",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_NivelesAcceso_IDNivelAcceso",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_PerfilesPuesto_IDPerfilPuesto",
                table: "ModulosAcceso");

            migrationBuilder.DropForeignKey(
                name: "FK_SucursalesInventario_Inventario_NoParte",
                table: "SucursalesInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_SucursalesInventario_Sucursales_IDSucursal",
                table: "SucursalesInventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_PerfilesPuesto_IDPerfilPuesto",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosSucursales_Sucursales_IDSucursal",
                table: "UsuariosSucursales");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosSucursales_Usuarios_IDUsuario",
                table: "UsuariosSucursales");

            migrationBuilder.DropIndex(
                name: "IX_SucursalesInventario_NoParte",
                table: "SucursalesInventario");

            migrationBuilder.DropIndex(
                name: "IX_LogInventario_NoParte",
                table: "LogInventario");

            migrationBuilder.DropColumn(
                name: "NoParte",
                table: "SucursalesInventario");

            migrationBuilder.DropColumn(
                name: "NoParte",
                table: "LogInventario");

            migrationBuilder.RenameColumn(
                name: "IDUsuario",
                table: "UsuariosSucursales",
                newName: "UsuarioIDUsuario");

            migrationBuilder.RenameColumn(
                name: "IDSucursal",
                table: "UsuariosSucursales",
                newName: "SucursalIDSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosSucursales_IDUsuario",
                table: "UsuariosSucursales",
                newName: "IX_UsuariosSucursales_UsuarioIDUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosSucursales_IDSucursal",
                table: "UsuariosSucursales",
                newName: "IX_UsuariosSucursales_SucursalIDSucursal");

            migrationBuilder.RenameColumn(
                name: "IDPerfilPuesto",
                table: "Usuarios",
                newName: "PerfilPuestoIDPerfilPuesto");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_IDPerfilPuesto",
                table: "Usuarios",
                newName: "IX_Usuarios_PerfilPuestoIDPerfilPuesto");

            migrationBuilder.RenameColumn(
                name: "IDSucursal",
                table: "SucursalesInventario",
                newName: "SucursalIDSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_SucursalesInventario_IDSucursal",
                table: "SucursalesInventario",
                newName: "IX_SucursalesInventario_SucursalIDSucursal");

            migrationBuilder.RenameColumn(
                name: "IDPerfilPuesto",
                table: "ModulosAcceso",
                newName: "PerfilPuestoIDPerfilPuesto");

            migrationBuilder.RenameColumn(
                name: "IDNivelAcceso",
                table: "ModulosAcceso",
                newName: "NivelAcceso1");

            migrationBuilder.RenameColumn(
                name: "IDModulo",
                table: "ModulosAcceso",
                newName: "ModuloIDModulo");

            migrationBuilder.RenameIndex(
                name: "IX_ModulosAcceso_IDPerfilPuesto",
                table: "ModulosAcceso",
                newName: "IX_ModulosAcceso_PerfilPuestoIDPerfilPuesto");

            migrationBuilder.RenameIndex(
                name: "IX_ModulosAcceso_IDNivelAcceso",
                table: "ModulosAcceso",
                newName: "IX_ModulosAcceso_NivelAcceso1");

            migrationBuilder.RenameIndex(
                name: "IX_ModulosAcceso_IDModulo",
                table: "ModulosAcceso",
                newName: "IX_ModulosAcceso_ModuloIDModulo");

            migrationBuilder.RenameColumn(
                name: "IDModuloCategoria",
                table: "Modulos",
                newName: "ModuloCategoriaIDModuloCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_Modulos_IDModuloCategoria",
                table: "Modulos",
                newName: "IX_Modulos_ModuloCategoriaIDModuloCategoria");

            migrationBuilder.RenameColumn(
                name: "IDUsuario",
                table: "LogInventario",
                newName: "TipoMovimientoIDTipoMovimientoInventario");

            migrationBuilder.RenameColumn(
                name: "IDTipoMovimiento",
                table: "LogInventario",
                newName: "SucursalIDSucursal");

            migrationBuilder.RenameColumn(
                name: "IDSucursal",
                table: "LogInventario",
                newName: "QuienRealizaIDUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_LogInventario_IDUsuario",
                table: "LogInventario",
                newName: "IX_LogInventario_TipoMovimientoIDTipoMovimientoInventario");

            migrationBuilder.RenameIndex(
                name: "IX_LogInventario_IDTipoMovimiento",
                table: "LogInventario",
                newName: "IX_LogInventario_SucursalIDSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_LogInventario_IDSucursal",
                table: "LogInventario",
                newName: "IX_LogInventario_QuienRealizaIDUsuario");

            migrationBuilder.RenameColumn(
                name: "IDUnidad",
                table: "Inventario",
                newName: "UnidadIDUnidad");

            migrationBuilder.RenameIndex(
                name: "IX_Inventario_IDUnidad",
                table: "Inventario",
                newName: "IX_Inventario_UnidadIDUnidad");

            migrationBuilder.AddColumn<string>(
                name: "ProductoNoParte",
                table: "SucursalesInventario",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductoNoParte",
                table: "LogInventario",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SucursalesInventario_ProductoNoParte",
                table: "SucursalesInventario",
                column: "ProductoNoParte");

            migrationBuilder.CreateIndex(
                name: "IX_LogInventario_ProductoNoParte",
                table: "LogInventario",
                column: "ProductoNoParte");

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
                name: "FK_LogInventario_TiposMovimientosInventario_TipoMovimientoIDTipoMovimientoInventario",
                table: "LogInventario",
                column: "TipoMovimientoIDTipoMovimientoInventario",
                principalTable: "TiposMovimientosInventario",
                principalColumn: "IDTipoMovimientoInventario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogInventario_Usuarios_QuienRealizaIDUsuario",
                table: "LogInventario",
                column: "QuienRealizaIDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
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
                name: "FK_ModulosAcceso_NivelesAcceso_NivelAcceso1",
                table: "ModulosAcceso",
                column: "NivelAcceso1",
                principalTable: "NivelesAcceso",
                principalColumn: "NivelAcceso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_PerfilesPuesto_PerfilPuestoIDPerfilPuesto",
                table: "ModulosAcceso",
                column: "PerfilPuestoIDPerfilPuesto",
                principalTable: "PerfilesPuesto",
                principalColumn: "IDPerfilPuesto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SucursalesInventario_Inventario_ProductoNoParte",
                table: "SucursalesInventario",
                column: "ProductoNoParte",
                principalTable: "Inventario",
                principalColumn: "NoParte");

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
    }
}
