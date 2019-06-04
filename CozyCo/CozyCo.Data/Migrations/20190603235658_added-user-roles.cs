using Microsoft.EntityFrameworkCore.Migrations;

namespace CozyCo.Data.Migrations
{
    public partial class addeduserroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aa1e8816-c057-4b75-965f-efaf205ad981", "4bba918f-c9f9-4e2f-b976-4add22f5237d", "Tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b1bb53e2-72ca-4dfb-8069-441c50322b77", "a7c6bfe7-f81d-421b-bb0a-0b1061094936", "Landlord", "LANDLORD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa1e8816-c057-4b75-965f-efaf205ad981");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1bb53e2-72ca-4dfb-8069-441c50322b77");
        }
    }
}
