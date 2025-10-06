using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayCareProject.Migrations
{
    /// <inheritdoc />
    public partial class newDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Admins",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Admins",
                newName: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Admins",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Admins",
                newName: "Id");
        }
    }
}
