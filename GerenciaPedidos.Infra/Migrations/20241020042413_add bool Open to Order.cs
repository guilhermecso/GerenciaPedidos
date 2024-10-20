using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciaPedidos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addboolOpentoOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Open",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Open",
                table: "Orders");
        }
    }
}
