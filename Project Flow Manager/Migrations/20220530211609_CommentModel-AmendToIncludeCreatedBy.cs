using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class CommentModelAmendToIncludeCreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Comment");
        }
    }
}
