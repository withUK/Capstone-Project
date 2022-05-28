using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class UpdatedModelRecommendationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technology_Recommendation_RecommendationId",
                table: "Technology");

            migrationBuilder.DropIndex(
                name: "IX_Technology_RecommendationId",
                table: "Technology");

            migrationBuilder.DropColumn(
                name: "RecommendationId",
                table: "Technology");

            migrationBuilder.AddColumn<int>(
                name: "RecommendationId",
                table: "TechnologyResource",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyResource_RecommendationId",
                table: "TechnologyResource",
                column: "RecommendationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyResource_Recommendation_RecommendationId",
                table: "TechnologyResource",
                column: "RecommendationId",
                principalTable: "Recommendation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyResource_Recommendation_RecommendationId",
                table: "TechnologyResource");

            migrationBuilder.DropIndex(
                name: "IX_TechnologyResource_RecommendationId",
                table: "TechnologyResource");

            migrationBuilder.DropColumn(
                name: "RecommendationId",
                table: "TechnologyResource");

            migrationBuilder.AddColumn<int>(
                name: "RecommendationId",
                table: "Technology",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technology_RecommendationId",
                table: "Technology",
                column: "RecommendationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technology_Recommendation_RecommendationId",
                table: "Technology",
                column: "RecommendationId",
                principalTable: "Recommendation",
                principalColumn: "Id");
        }
    }
}
