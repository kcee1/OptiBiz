using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddeedTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVerifications_AspNetUsers_UserId1",
                table: "UserVerifications");

            migrationBuilder.DropIndex(
                name: "IX_UserVerifications_UserId1",
                table: "UserVerifications");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserVerifications");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserVerifications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserVerifications_UserId",
                table: "UserVerifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVerifications_AspNetUsers_UserId",
                table: "UserVerifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVerifications_AspNetUsers_UserId",
                table: "UserVerifications");

            migrationBuilder.DropIndex(
                name: "IX_UserVerifications_UserId",
                table: "UserVerifications");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserVerifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserVerifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserVerifications_UserId1",
                table: "UserVerifications",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVerifications_AspNetUsers_UserId1",
                table: "UserVerifications",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
