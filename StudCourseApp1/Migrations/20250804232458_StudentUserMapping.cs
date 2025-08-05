using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudCourseApp1.Migrations
{
    /// <inheritdoc />
    public partial class StudentUserMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ApplicationUserId",
                table: "Students",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_ApplicationUserId",
                table: "Students",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_ApplicationUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ApplicationUserId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Students",
                newName: "Name");
        }
    }
}
