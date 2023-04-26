using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CesgranSec.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertieInCryptFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "CryptoFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "CryptoFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "CryptoFiles");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "CryptoFiles");
        }
    }
}
