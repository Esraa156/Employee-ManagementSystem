using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$HDSiBOZGvPM7KTCyS5ATE.OwYwZvDDow9/.wwL4NtrmzspcQt1EwO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$vtWD2F1W60fO2hzgvBSj1u1/oKbEOoumKEBdK2/0/m1G2HnrXPJVS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$cAGxvda7rBD17yvaGuy2GuupY6uaIuaou1ThgygD2vk2Bhd1yTFOS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$yYnaUVqjn.8XcuLY.qkztOS9R2F7Gs5Q1bKYJYP6ao0Y3kpPsBbBO");
        }
    }
}
