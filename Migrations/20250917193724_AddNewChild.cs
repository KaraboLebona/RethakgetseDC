using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayCareProject.Migrations
{
    /// <inheritdoc />
    public partial class AddNewChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChildApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cell = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinRelation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildApplications", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildApplications");
        }
    }
}
