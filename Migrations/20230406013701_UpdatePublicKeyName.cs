using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CesgranSec.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePublicKeyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicKeyModulos",
                table: "AspNetUsers",
                newName: "PublicKeyModulus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicKeyModulus",
                table: "AspNetUsers",
                newName: "PublicKeyModulos");
        }
    }
}
