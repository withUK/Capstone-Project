using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class ModelUpdateRecommendation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.AddColumn<int>(
                name: "RecommendationId",
                table: "TeamResource",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamResource_RecommendationId",
                table: "TeamResource",
                column: "RecommendationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResource_Recommendation_RecommendationId",
                table: "TeamResource",
                column: "RecommendationId",
                principalTable: "Recommendation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamResource_Recommendation_RecommendationId",
                table: "TeamResource");

            migrationBuilder.DropIndex(
                name: "IX_TeamResource_RecommendationId",
                table: "TeamResource");

            migrationBuilder.DropColumn(
                name: "RecommendationId",
                table: "TeamResource");

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecommendationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Recommendation_RecommendationId",
                        column: x => x.RecommendationId,
                        principalTable: "Recommendation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_RecommendationId",
                table: "Team",
                column: "RecommendationId");
        }
    }
}
