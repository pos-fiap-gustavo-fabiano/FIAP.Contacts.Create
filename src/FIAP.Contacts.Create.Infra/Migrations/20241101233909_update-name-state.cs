using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.Contacts.Create.Infra.Migrations
{
    /// <inheritdoc />
    public partial class updatenamestate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Addresses",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Addresses",
                newName: "Country");
        }
    }
}
