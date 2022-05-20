using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class InitialCreateTeamResourceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamResource_Team_TeamId",
                table: "TeamResource");

            migrationBuilder.DropIndex(
                name: "IX_TeamResource_TeamId",
                table: "TeamResource");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "TeamResource");

            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "TeamResource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team",
                table: "TeamResource");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "TeamResource",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamResource_TeamId",
                table: "TeamResource",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResource_Team_TeamId",
                table: "TeamResource",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
