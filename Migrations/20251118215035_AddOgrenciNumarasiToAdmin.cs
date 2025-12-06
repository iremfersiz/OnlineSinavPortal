using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSinavPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddOgrenciNumarasiToAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OgrenciNumarasi",
                table: "Admins",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OgrenciNumarasi",
                table: "Admins");
        }
    }
}
