using Microsoft.EntityFrameworkCore.Migrations;

namespace Gold_Quiz.DataModel.Migrations
{
    public partial class mig51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "UserType",
                table: "CenterUsers_Tbl",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "CenterUsers_Tbl");
        }
    }
}
