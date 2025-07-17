using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domin_Controle_Back.Migrations
{
    /// <inheritdoc />
    public partial class ExamAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rni",
                table: "Exams",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "Hematocrito",
                table: "Exams",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rni",
                table: "Exams",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Hematocrito",
                table: "Exams",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }
    }
}
