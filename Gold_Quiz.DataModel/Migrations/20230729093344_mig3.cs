using Microsoft.EntityFrameworkCore.Migrations;

namespace Gold_Quiz.DataModel.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Course_Tbl",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_Tbl_UserID",
                table: "Course_Tbl",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Tbl_Users_Tbl_UserID",
                table: "Course_Tbl",
                column: "UserID",
                principalTable: "Users_Tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Tbl_Users_Tbl_UserID",
                table: "Course_Tbl");

            migrationBuilder.DropIndex(
                name: "IX_Course_Tbl_UserID",
                table: "Course_Tbl");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Course_Tbl");
        }
    }
}
