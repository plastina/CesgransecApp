using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CesgranSec.Migrations
{
    /// <inheritdoc />
    public partial class AddKeysToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicKey",
                table: "AspNetUsers",
                newName: "PublicKeyModulos");

            migrationBuilder.RenameColumn(
                name: "PrivateKey",
                table: "AspNetUsers",
                newName: "PublicKeyExponent");

            migrationBuilder.AddColumn<string>(
                name: "PrivateKeyD",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrivateKeyModulus",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateKeyD",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrivateKeyModulus",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PublicKeyModulos",
                table: "AspNetUsers",
                newName: "PublicKey");

            migrationBuilder.RenameColumn(
                name: "PublicKeyExponent",
                table: "AspNetUsers",
                newName: "PrivateKey");
        }
    }
}
