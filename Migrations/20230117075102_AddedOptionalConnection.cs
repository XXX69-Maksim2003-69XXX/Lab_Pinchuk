using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinchuckLab.Migrations
{
    /// <inheritdoc />
    public partial class AddedOptionalConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
