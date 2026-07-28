using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGP.Platform.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddViewComponentsFieldsForPostsAndEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ViewComponent",
                schema: "platform",
                table: "Posts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViewComponent",
                schema: "platform",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewComponent",
                schema: "platform",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ViewComponent",
                schema: "platform",
                table: "Events");
        }
    }
}
