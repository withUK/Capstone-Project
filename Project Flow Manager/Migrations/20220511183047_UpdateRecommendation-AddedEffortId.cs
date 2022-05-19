using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class UpdateRecommendationAddedEffortId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecommendationId",
                table: "Technology",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Recommendation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Recommendation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EffortId",
                table: "Recommendation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Effort",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Measure = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Effort", x => x.Id);
                });

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
                name: "IX_Technology_RecommendationId",
                table: "Technology",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_EffortId",
                table: "Recommendation",
                column: "EffortId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_RecommendationId",
                table: "Team",
                column: "RecommendationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendation_Effort_EffortId",
                table: "Recommendation",
                column: "EffortId",
                principalTable: "Effort",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Technology_Recommendation_RecommendationId",
                table: "Technology",
                column: "RecommendationId",
                principalTable: "Recommendation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendation_Effort_EffortId",
                table: "Recommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_Technology_Recommendation_RecommendationId",
                table: "Technology");

            migrationBuilder.DropTable(
                name: "Effort");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Technology_RecommendationId",
                table: "Technology");

            migrationBuilder.DropIndex(
                name: "IX_Recommendation_EffortId",
                table: "Recommendation");

            migrationBuilder.DropColumn(
                name: "RecommendationId",
                table: "Technology");

            migrationBuilder.DropColumn(
                name: "EffortId",
                table: "Recommendation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Recommendation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Recommendation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
