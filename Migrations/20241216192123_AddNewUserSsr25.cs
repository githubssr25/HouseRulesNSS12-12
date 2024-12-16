using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRules.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUserSsr25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_IdentityUserId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "UserProfiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserProfiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserProfiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "UserProfiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a5a70c3-f9d4-4631-9dd7-a3a8d7e065cb", "AQAAAAIAAYagAAAAEHi3YTB8FYeDTKsbH/lV7u5tXc3mk23UYA+Brb+7Nl42hUo7wVQtzS5+sOEqSccMWA==", "0dafc71b-349c-4424-a297-5d61d5c0e46b" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "456e7b9a-a12f-4bcb-a9e7-a2c5e764d234", 0, "2f5d0cf2-f6e9-40b8-9db1-34b53c1a9532", "sr25practice@example.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEHRayzGu0seokYHyYYGvokw5PzSwwt+L/0feeRdt4iHXEL7dozx0p8Co8dDNiu5C8g==", null, false, "b3f069c1-9c58-4f4c-886f-51b83f74494e", false, "ssr25practice" });

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 2, 13, 21, 23, 422, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 9, 13, 21, 23, 422, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 15, 13, 21, 23, 422, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 9, 13, 21, 23, 422, DateTimeKind.Local).AddTicks(1630));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", "456e7b9a-a12f-4bcb-a9e7-a2c5e764d234" });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Address", "Email", "FirstName", "IdentityUserId", "LastName" },
                values: new object[] { 3, "300 Admin Street", "sr25practice@example.com", "Secondssr25", "456e7b9a-a12f-4bcb-a9e7-a2c5e764d234", "Adminssr25" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_IdentityUserId",
                table: "UserProfiles",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_IdentityUserId",
                table: "UserProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", "456e7b9a-a12f-4bcb-a9e7-a2c5e764d234" });

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "456e7b9a-a12f-4bcb-a9e7-a2c5e764d234");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85e6a13a-903a-408d-b744-3cdaca229475", "AQAAAAIAAYagAAAAEK6ez4PFAUhUFYTQQZkB4s98yGNtkUesqL7hdSIcTp0t2lijU+eGGow2RrMcM0oCxQ==", "ab48be71-44c4-4cb8-afb6-f7d758e2229e" });

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 2, 10, 57, 31, 370, DateTimeKind.Local).AddTicks(9210));

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 9, 10, 57, 31, 370, DateTimeKind.Local).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 15, 10, 57, 31, 370, DateTimeKind.Local).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 9, 10, 57, 31, 370, DateTimeKind.Local).AddTicks(9300));

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_IdentityUserId",
                table: "UserProfiles",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
