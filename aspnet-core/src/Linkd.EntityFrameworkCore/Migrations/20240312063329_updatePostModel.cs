using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linkd.Migrations
{
    /// <inheritdoc />
    public partial class updatePostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Posts",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Posts",
                newName: "ImageUrls");
        }
    }
}
