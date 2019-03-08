using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkSupply.Persistance.SQL.Migrations
{
    public partial class AddAdminToAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "529c887a-5961-4842-8433-e3d0ef45a5e8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9607ef0a-c9e2-44f8-82cd-2971b3bcd88d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "be0b8664-711c-4a95-a505-19713b7e8ec7");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2263487a-f076-47f1-bdf4-95f8a2c11447", "AQAAAAEAACcQAAAAEDib/ygMdkmMscZ7mWLeR3nsbJhXpxnVcT3cEJ44/OeLRc+5NnvPzrf65oG+8XnYkA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "bde8c3a2-2276-4991-ba39-7faabe9cb9d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "379b3b8a-35be-4005-bdd4-292e7ae9b184");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6dcf53c2-b765-4cf4-963d-eadb428cde90");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0022cee1-e072-47d6-93ba-2113e87fb08d", "AQAAAAEAACcQAAAAEHMAHe3dggEH4DeLKGSqrZJHIKP5mRuA2rowOJWCc7I9DBjWll8ESH+S2+gma6q6Vg==" });
        }
    }
}
