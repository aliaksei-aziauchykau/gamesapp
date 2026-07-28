using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGP.Platform.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddNameFieldToContainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "platform",
                table: "Containers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "platform",
                table: "Containers");
        }
    }
}
