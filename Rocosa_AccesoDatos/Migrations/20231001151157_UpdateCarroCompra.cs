using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rocosa_AccesoDatos.Migrations
{
    public partial class UpdateCarroCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Categorias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_ProductoId",
                table: "Categorias",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Productos_ProductoId",
                table: "Categorias",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Productos_ProductoId",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_ProductoId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Categorias");

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
