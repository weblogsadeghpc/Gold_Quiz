using Microsoft.EntityFrameworkCore.Migrations;

namespace Gold_Quiz.DataModel.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Users_Tbl");

            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "Users_Tbl");

            migrationBuilder.CreateTable(
                name: "Centers_Tbl",
                columns: table => new
                {
                    CenterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterType = table.Column<byte>(type: "tinyint", nullable: false),
                    CenterAdminID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers_Tbl", x => x.CenterID);
                    table.ForeignKey(
                        name: "FK_Centers_Tbl_Users_Tbl_CenterAdminID",
                        column: x => x.CenterAdminID,
                        principalTable: "Users_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterUsers_Tbl",
                columns: table => new
                {
                    CenterUsersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterAdminID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CenterUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CenterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterUsers_Tbl", x => x.CenterUsersID);
                    table.ForeignKey(
                        name: "FK_CenterUsers_Tbl_Centers_Tbl_CenterID",
                        column: x => x.CenterID,
                        principalTable: "Centers_Tbl",
                        principalColumn: "CenterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterUsers_Tbl_Users_Tbl_CenterAdminID",
                        column: x => x.CenterAdminID,
                        principalTable: "Users_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterUsers_Tbl_Users_Tbl_CenterUserID",
                        column: x => x.CenterUserID,
                        principalTable: "Users_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCourses_Tbl",
                columns: table => new
                {
                    TeacherCourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    TeacherAdminID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CenterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourses_Tbl", x => x.TeacherCourseID);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Tbl_Centers_Tbl_CenterID",
                        column: x => x.CenterID,
                        principalTable: "Centers_Tbl",
                        principalColumn: "CenterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Tbl_Course_Tbl_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course_Tbl",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Tbl_Users_Tbl_TeacherAdminID",
                        column: x => x.TeacherAdminID,
                        principalTable: "Users_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Tbl_Users_Tbl_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Users_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Centers_Tbl_CenterAdminID",
                table: "Centers_Tbl",
                column: "CenterAdminID");

            migrationBuilder.CreateIndex(
                name: "IX_CenterUsers_Tbl_CenterAdminID",
                table: "CenterUsers_Tbl",
                column: "CenterAdminID");

            migrationBuilder.CreateIndex(
                name: "IX_CenterUsers_Tbl_CenterID",
                table: "CenterUsers_Tbl",
                column: "CenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CenterUsers_Tbl_CenterUserID",
                table: "CenterUsers_Tbl",
                column: "CenterUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_Tbl_CenterID",
                table: "TeacherCourses_Tbl",
                column: "CenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_Tbl_CourseID",
                table: "TeacherCourses_Tbl",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_Tbl_TeacherAdminID",
                table: "TeacherCourses_Tbl",
                column: "TeacherAdminID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_Tbl_TeacherID",
                table: "TeacherCourses_Tbl",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterUsers_Tbl");

            migrationBuilder.DropTable(
                name: "TeacherCourses_Tbl");

            migrationBuilder.DropTable(
                name: "Centers_Tbl");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Users_Tbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "CustomerType",
                table: "Users_Tbl",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
