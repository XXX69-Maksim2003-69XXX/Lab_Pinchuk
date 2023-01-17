using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinchuckLab.Migrations
{
    /// <inheritdoc />
    public partial class AddedOptionalConnectionV2 : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Payment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Clients_ClientId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_ClientId",
                table: "Payment");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ClientId",
                table: "Payment",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Clients_ClientId",
                table: "Payment",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
