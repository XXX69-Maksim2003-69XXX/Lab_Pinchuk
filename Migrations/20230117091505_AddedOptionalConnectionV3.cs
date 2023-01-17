using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinchuckLab.Migrations
{
    /// <inheritdoc />
    public partial class AddedOptionalConnectionV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Clients_ClientId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_ClientId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Payment");

            migrationBuilder.AddColumn<string>(
                name: "Passport",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Passport",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Clients_Passport",
                table: "Clients",
                column: "Passport");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Passport",
                table: "Payment",
                column: "Passport",
                unique: true,
                filter: "[Passport] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Clients_Passport",
                table: "Payment",
                column: "Passport",
                principalTable: "Clients",
                principalColumn: "Passport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Clients_Passport",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_Passport",
                table: "Payment");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Clients_Passport",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Passport",
                table: "Payment");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Payment",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Passport",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ClientId",
                table: "Payment",
                column: "ClientId",
                unique: true,
                filter: "[ClientId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Clients_ClientId",
                table: "Payment",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
