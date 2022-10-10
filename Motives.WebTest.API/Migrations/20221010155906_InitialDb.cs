using Microsoft.EntityFrameworkCore.Migrations;

namespace Motives.WebTest.API.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(maxLength: 2147483647, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    SubName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableUsers");
        }
    }
}
