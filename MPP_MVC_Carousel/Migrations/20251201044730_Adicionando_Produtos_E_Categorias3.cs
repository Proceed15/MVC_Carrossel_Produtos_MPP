using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPP_MVC_Carousel.Migrations
{
    /// <inheritdoc />
    public partial class Adicionando_Produtos_E_Categorias3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Categorias");
        }
    }
}
