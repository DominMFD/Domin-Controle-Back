using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domin_Controle_Back.Migrations
{
    /// <inheritdoc />
    public partial class AddMarevanToExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Marevan",
                table: "Exams",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marevan",
                table: "Exams");
        }
    }
}
