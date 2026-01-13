using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class LogicaInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_Modulos_ModuloIDModulo",
                table: "ModulosAcceso");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "ModulosCategorias");

            migrationBuilder.CreateTable(
                name: "ModulosCategoria",
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
                    table.PrimaryKey("PK_ModulosCategoria", x => x.IDModuloCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    IDSucursal = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.IDSucursal);
                });

            migrationBuilder.CreateTable(
                name: "TiposMovimientosInventario",
                columns: table => new
                {
                    IDTipoMovimientoInventario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    EntradaSalida = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMovimientosInventario", x => x.IDTipoMovimientoInventario);
                });

            migrationBuilder.CreateTable(
                name: "Unidad",
                columns: table => new
                {
                    IDUnidad = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Abreviacion = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidad", x => x.IDUnidad);
                });

            migrationBuilder.CreateTable(
                name: "Modulo",
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
                    table.PrimaryKey("PK_Modulo", x => x.IDModulo);
                    table.ForeignKey(
                        name: "FK_Modulo_ModulosCategoria_ModuloCategoriaIDModuloCategoria",
                        column: x => x.ModuloCategoriaIDModuloCategoria,
                        principalTable: "ModulosCategoria",
                        principalColumn: "IDModuloCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioSucursal",
                columns: table => new
                {
                    IDSucursalUsuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    UsuarioIDUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    SucursalIDSucursal = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioSucursal", x => x.IDSucursalUsuario);
                    table.ForeignKey(
                        name: "FK_UsuarioSucursal_Sucursal_SucursalIDSucursal",
                        column: x => x.SucursalIDSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "IDSucursal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioSucursal_Usuarios_UsuarioIDUsuario",
                        column: x => x.UsuarioIDUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    NoParte = table.Column<string>(type: "TEXT", nullable: false),
                    NombreProducto = table.Column<string>(type: "TEXT", nullable: false),
                    DescripcionProducto = table.Column<string>(type: "TEXT", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnidadIDUnidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.NoParte);
                    table.ForeignKey(
                        name: "FK_Inventario_Unidad_UnidadIDUnidad",
                        column: x => x.UnidadIDUnidad,
                        principalTable: "Unidad",
                        principalColumn: "IDUnidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogInventario",
                columns: table => new
                {
                    IDLogInventario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false),
                    QuienRealizaIDUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoNoParte = table.Column<string>(type: "TEXT", nullable: false),
                    SucursalIDSucursal = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoMovimientoIDTipoMovimientoInventario = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogInventario", x => x.IDLogInventario);
                    table.ForeignKey(
                        name: "FK_LogInventario_Inventario_ProductoNoParte",
                        column: x => x.ProductoNoParte,
                        principalTable: "Inventario",
                        principalColumn: "NoParte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogInventario_Sucursal_SucursalIDSucursal",
                        column: x => x.SucursalIDSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "IDSucursal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogInventario_TiposMovimientosInventario_TipoMovimientoIDTipoMovimientoInventario",
                        column: x => x.TipoMovimientoIDTipoMovimientoInventario,
                        principalTable: "TiposMovimientosInventario",
                        principalColumn: "IDTipoMovimientoInventario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogInventario_Usuarios_QuienRealizaIDUsuario",
                        column: x => x.QuienRealizaIDUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SucursalesInventario",
                columns: table => new
                {
                    IDSucursalInventario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Existencia = table.Column<decimal>(type: "TEXT", nullable: false),
                    UmbralExistencia = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProductoNoParte = table.Column<string>(type: "TEXT", nullable: true),
                    SucursalIDSucursal = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucursalesInventario", x => x.IDSucursalInventario);
                    table.ForeignKey(
                        name: "FK_SucursalesInventario_Inventario_ProductoNoParte",
                        column: x => x.ProductoNoParte,
                        principalTable: "Inventario",
                        principalColumn: "NoParte");
                    table.ForeignKey(
                        name: "FK_SucursalesInventario_Sucursal_SucursalIDSucursal",
                        column: x => x.SucursalIDSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "IDSucursal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_UnidadIDUnidad",
                table: "Inventario",
                column: "UnidadIDUnidad");

            migrationBuilder.CreateIndex(
                name: "IX_LogInventario_ProductoNoParte",
                table: "LogInventario",
                column: "ProductoNoParte");

            migrationBuilder.CreateIndex(
                name: "IX_LogInventario_QuienRealizaIDUsuario",
                table: "LogInventario",
                column: "QuienRealizaIDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_LogInventario_SucursalIDSucursal",
                table: "LogInventario",
                column: "SucursalIDSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_LogInventario_TipoMovimientoIDTipoMovimientoInventario",
                table: "LogInventario",
                column: "TipoMovimientoIDTipoMovimientoInventario");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_ModuloCategoriaIDModuloCategoria",
                table: "Modulo",
                column: "ModuloCategoriaIDModuloCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalesInventario_ProductoNoParte",
                table: "SucursalesInventario",
                column: "ProductoNoParte");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalesInventario_SucursalIDSucursal",
                table: "SucursalesInventario",
                column: "SucursalIDSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSucursal_SucursalIDSucursal",
                table: "UsuarioSucursal",
                column: "SucursalIDSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSucursal_UsuarioIDUsuario",
                table: "UsuarioSucursal",
                column: "UsuarioIDUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_Modulo_ModuloIDModulo",
                table: "ModulosAcceso",
                column: "ModuloIDModulo",
                principalTable: "Modulo",
                principalColumn: "IDModulo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModulosAcceso_Modulo_ModuloIDModulo",
                table: "ModulosAcceso");

            migrationBuilder.DropTable(
                name: "LogInventario");

            migrationBuilder.DropTable(
                name: "Modulo");

            migrationBuilder.DropTable(
                name: "SucursalesInventario");

            migrationBuilder.DropTable(
                name: "UsuarioSucursal");

            migrationBuilder.DropTable(
                name: "TiposMovimientosInventario");

            migrationBuilder.DropTable(
                name: "ModulosCategoria");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Unidad");

            migrationBuilder.CreateTable(
                name: "ModulosCategorias",
                columns: table => new
                {
                    IDModuloCategoria = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Icono = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulosCategorias", x => x.IDModuloCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    IDModulo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModuloCategoriaIDModuloCategoria = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Icono = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_ModuloCategoriaIDModuloCategoria",
                table: "Modulos",
                column: "ModuloCategoriaIDModuloCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_ModulosAcceso_Modulos_ModuloIDModulo",
                table: "ModulosAcceso",
                column: "ModuloIDModulo",
                principalTable: "Modulos",
                principalColumn: "IDModulo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
