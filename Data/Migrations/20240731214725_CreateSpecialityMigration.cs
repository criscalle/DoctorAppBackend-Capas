using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateSpecialityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "username");

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    namespeciality = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "UserName");
        }
    }
}
