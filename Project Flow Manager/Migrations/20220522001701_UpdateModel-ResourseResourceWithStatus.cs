using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class UpdateModelResourseResourceWithStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ResourceRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ResourceRequest");
        }
    }
}
