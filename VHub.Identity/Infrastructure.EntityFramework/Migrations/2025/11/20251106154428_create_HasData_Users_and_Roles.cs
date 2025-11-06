using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.EntityFramework.Migrations._2025._11
{
    /// <inheritdoc />
    public partial class create_HasData_Users_and_Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("2c405bc9-8873-4491-b6af-537b901a8e52"), null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("473cb633-8f80-4910-bf3f-dd45383b28a6"), 0, "b507ce46-bf48-412b-b1c1-4187cd5940b7", "Roman@gmail.com", false, true, null, "ROMAN@GMAIL.COM", "ROMAN", "AQAAAAIAAYagAAAAEJclNaSM+VykPmT4NxD2EXDRjW2KXy8DYo8+9UDEtBJAsUzrIkICLjG7HHYWI8hJNQ==", null, false, "F7VJLSKLX3FVHCCBMCSCU7XKTTC4WZKZ", false, "Roman" },
                    { new Guid("6ab774a9-d782-4223-999d-ffe4659eb780"), 0, "b507ce46-bf48-412b-b1c1-4187cd5940b7", "Ivan@gmail.com", false, true, null, "IVAN@GMAIL.COM", "IVAN", "AQAAAAIAAYagAAAAEJclNaSM+VykPmT4NxD2EXDRjW2KXy8DYo8+9UDEtBJAsUzrIkICLjG7HHYWI8hJNQ==", null, false, "F7VJLSKLX3FVHCCBMCSCU7XKTTC4WZKZ", false, "Ivan" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("2c405bc9-8873-4491-b6af-537b901a8e52"), new Guid("473cb633-8f80-4910-bf3f-dd45383b28a6") },
                    { new Guid("2c405bc9-8873-4491-b6af-537b901a8e52"), new Guid("6ab774a9-d782-4223-999d-ffe4659eb780") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2c405bc9-8873-4491-b6af-537b901a8e52"), new Guid("473cb633-8f80-4910-bf3f-dd45383b28a6") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2c405bc9-8873-4491-b6af-537b901a8e52"), new Guid("6ab774a9-d782-4223-999d-ffe4659eb780") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c405bc9-8873-4491-b6af-537b901a8e52"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("473cb633-8f80-4910-bf3f-dd45383b28a6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6ab774a9-d782-4223-999d-ffe4659eb780"));
        }
    }
}
