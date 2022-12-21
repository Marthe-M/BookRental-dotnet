using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRentaldotnet.Migrations
{
    /// <inheritdoc />
    public partial class onetomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reservations",
                newName: "ReservationId");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Reservations",
                newName: "id");
        }
    }
}
