using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRentaldotnet.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Books_BookId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reservations",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Reservations",
                newName: "bookId");

            migrationBuilder.RenameColumn(
                name: "Approved",
                table: "Reservations",
                newName: "approved");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservations",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                newName: "IX_Reservations_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_BookId",
                table: "Reservations",
                newName: "IX_Reservations_bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Books_bookId",
                table: "Reservations",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_userId",
                table: "Reservations",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Books_bookId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_userId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Reservations",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "bookId",
                table: "Reservations",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "approved",
                table: "Reservations",
                newName: "Approved");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reservations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_userId",
                table: "Reservations",
                newName: "IX_Reservations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_bookId",
                table: "Reservations",
                newName: "IX_Reservations_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Books_BookId",
                table: "Reservations",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
