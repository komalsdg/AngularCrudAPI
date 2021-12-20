using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularCrudAPI.Migrations
{
    public partial class seedgroupdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "groups",
                columns: new[] { "GroupId", "GroupName" },
                values: new object[] { 1, "Group1" });

            migrationBuilder.InsertData(
                table: "groups",
                columns: new[] { "GroupId", "GroupName" },
                values: new object[] { 2, "Group2" });

            migrationBuilder.InsertData(
                table: "groups",
                columns: new[] { "GroupId", "GroupName" },
                values: new object[] { 3, "Group3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "groups",
                keyColumn: "GroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "groups",
                keyColumn: "GroupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "groups",
                keyColumn: "GroupId",
                keyValue: 3);
        }
    }
}
