using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CozyCo.Data.Migrations
{
    public partial class addpropertytypewithseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Condo" });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { 2, "Single Family Home" });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { 3, "Duplex" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyTypes");
        }
    }
}
