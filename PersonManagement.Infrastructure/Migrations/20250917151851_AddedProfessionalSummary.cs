using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedProfessionalSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfessionalSummary",
                table: "Persons",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessionalSummary",
                table: "Persons");
        }
    }
}
