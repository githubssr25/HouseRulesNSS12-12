using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRules.Migrations
{
    /// <inheritdoc />
    public partial class AddSeededUsersAttempt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename and update primary keys for Chores, ChoreAssignments, and ChoreCompletions
            migrationBuilder.DropForeignKey(name: "FK_ChoreAssignment_Chore_ChoreId", table: "ChoreAssignment");
            migrationBuilder.DropForeignKey(name: "FK_ChoreAssignment_UserProfiles_UserProfileId", table: "ChoreAssignment");
            migrationBuilder.DropForeignKey(name: "FK_ChoreCompletion_Chore_ChoreId", table: "ChoreCompletion");
            migrationBuilder.DropForeignKey(name: "FK_ChoreCompletion_UserProfiles_UserProfileId", table: "ChoreCompletion");

            migrationBuilder.DropPrimaryKey(name: "PK_ChoreCompletion", table: "ChoreCompletion");
            migrationBuilder.DropPrimaryKey(name: "PK_ChoreAssignment", table: "ChoreAssignment");
            migrationBuilder.DropPrimaryKey(name: "PK_Chore", table: "Chore");

            migrationBuilder.RenameTable(name: "ChoreCompletion", newName: "ChoreCompletions");
            migrationBuilder.RenameTable(name: "ChoreAssignment", newName: "ChoreAssignments");
            migrationBuilder.RenameTable(name: "Chore", newName: "Chores");

            migrationBuilder.RenameIndex(name: "IX_ChoreCompletion_UserProfileId", table: "ChoreCompletions", newName: "IX_ChoreCompletions_UserProfileId");
            migrationBuilder.RenameIndex(name: "IX_ChoreCompletion_ChoreId", table: "ChoreCompletions", newName: "IX_ChoreCompletions_ChoreId");
            migrationBuilder.RenameIndex(name: "IX_ChoreAssignment_UserProfileId", table: "ChoreAssignments", newName: "IX_ChoreAssignments_UserProfileId");
            migrationBuilder.RenameIndex(name: "IX_ChoreAssignment_ChoreId", table: "ChoreAssignments", newName: "IX_ChoreAssignments_ChoreId");

            migrationBuilder.AddPrimaryKey(name: "PK_ChoreCompletions", table: "ChoreCompletions", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_ChoreAssignments", table: "ChoreAssignments", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Chores", table: "Chores", column: "Id");

            // Insert the User first to ensure it exists for the UserRoles insert
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] 
                { 
                    "31e1f2fb-4898-4079-a0ff-79ea232d3445", 
                    0, 
                    "108b7fc5-d86c-495e-aeb5-fb468b66186a", 
                    "secondadmin@example.com", 
                    false, 
                    false, 
                    null, 
                    null, 
                    null, 
                    "AQAAAAIAAYagAAAAEPYsq36sTlgDdLkEzoAZdL6fEW1XX5mzSoMW7WelvU8a1aBcKeLQiaT/H8/b3OGWuQ==", 
                    null, 
                    false, 
                    "982a2eaa-0df1-4efa-b170-6c651063bc06", 
                    false, 
                    "secondadmin" 
                });

            // Insert Role for the newly created user
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", "31e1f2fb-4898-4079-a0ff-79ea232d3445" });

            // Update ChoreCompletions
            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CompletedOn",
                value: new DateTime(2024, 11, 29, 11, 27, 9, 878, DateTimeKind.Local).AddTicks(5040));

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 6, 11, 27, 9, 878, DateTimeKind.Local).AddTicks(5130));

            // Insert UserProfile linked to the new user
            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Address", "Email", "FirstName", "IdentityUserId", "LastName" },
                values: new object[] { 3, "300 Admin Street", "secondadmin@example.com", "Second", "31e1f2fb-4898-4079-a0ff-79ea232d3445", "Admin" });

            // Add Foreign Keys
            migrationBuilder.AddForeignKey(name: "FK_ChoreAssignments_Chores_ChoreId", table: "ChoreAssignments", column: "ChoreId", principalTable: "Chores", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_ChoreAssignments_UserProfiles_UserProfileId", table: "ChoreAssignments", column: "UserProfileId", principalTable: "UserProfiles", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_ChoreCompletions_Chores_ChoreId", table: "ChoreCompletions", column: "ChoreId", principalTable: "Chores", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_ChoreCompletions_UserProfiles_UserProfileId", table: "ChoreCompletions", column: "UserProfileId", principalTable: "UserProfiles", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", "31e1f2fb-4898-4079-a0ff-79ea232d3445" });

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31e1f2fb-4898-4079-a0ff-79ea232d3445");
        }
    }
}
