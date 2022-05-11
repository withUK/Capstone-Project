using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class UpdateRecommendationAddedEffortIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendation_Effort_EffortId",
                table: "Recommendation");

            migrationBuilder.AlterColumn<int>(
                name: "EffortId",
                table: "Recommendation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendation_Effort_EffortId",
                table: "Recommendation",
                column: "EffortId",
                principalTable: "Effort",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendation_Effort_EffortId",
                table: "Recommendation");

            migrationBuilder.AlterColumn<int>(
                name: "EffortId",
                table: "Recommendation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendation_Effort_EffortId",
                table: "Recommendation",
                column: "EffortId",
                principalTable: "Effort",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
