using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class ProjectAssessmentReportAddedChosenRecomendation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChosenRecommendationId",
                table: "ProjectAssessmentReport",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChosenRecommendationId",
                table: "ProjectAssessmentReport");
        }
    }
}
