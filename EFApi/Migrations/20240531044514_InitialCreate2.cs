using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Author_AuthorId1",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_AuthorId1",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_Author_PostId",
                table: "Author",
                column: "PostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Post_PostId",
                table: "Author",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Post_PostId",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_PostId",
                table: "Author");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Post",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId1",
                table: "Post",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_AuthorId1",
                table: "Post",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Author_AuthorId1",
                table: "Post",
                column: "AuthorId1",
                principalTable: "Author",
                principalColumn: "AuthorId");
        }
    }
}
