using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Admin User", "$2a$11$cAGxvda7rBD17yvaGuy2GuupY6uaIuaou1ThgygD2vk2Bhd1yTFOS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Normal User", "$2a$11$yYnaUVqjn.8XcuLY.qkztOS9R2F7Gs5Q1bKYJYP6ao0Y3kpPsBbBO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$shYwMtLyYpocWOdszl5Kfe4aMHivHpLRvZYaON2JgrzTH75hWpaHC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$cM7wuu0DnGSjgGkLyAeSuuivyLeovdPYtzsZga.nBWFtf0e03kud2");
        }
    }
}
