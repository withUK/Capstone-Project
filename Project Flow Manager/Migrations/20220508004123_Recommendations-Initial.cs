using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class RecommendationsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecommendationId",
                table: "ProcessStep",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStep_RecommendationId",
                table: "ProcessStep",
                column: "RecommendationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessStep_Recommendation_RecommendationId",
                table: "ProcessStep",
                column: "RecommendationId",
                principalTable: "Recommendation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessStep_Recommendation_RecommendationId",
                table: "ProcessStep");

            migrationBuilder.DropIndex(
                name: "IX_ProcessStep_RecommendationId",
                table: "ProcessStep");

            migrationBuilder.DropColumn(
                name: "RecommendationId",
                table: "ProcessStep");
        }
    }
}
