using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class ResourceRequestInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResourceRequestId",
                table: "Technology",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResourceRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectAssessmentId = table.Column<int>(type: "int", nullable: false),
                    ProjectAssessmentReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceRequest_ProjectAssessmentReport_ProjectAssessmentReportId",
                        column: x => x.ProjectAssessmentReportId,
                        principalTable: "ProjectAssessmentReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    ResourceRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamResource_ResourceRequest_ResourceRequestId",
                        column: x => x.ResourceRequestId,
                        principalTable: "ResourceRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamResource_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Technology_ResourceRequestId",
                table: "Technology",
                column: "ResourceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRequest_ProjectAssessmentReportId",
                table: "ResourceRequest",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamResource_ResourceRequestId",
                table: "TeamResource",
                column: "ResourceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamResource_TeamId",
                table: "TeamResource",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technology_ResourceRequest_ResourceRequestId",
                table: "Technology",
                column: "ResourceRequestId",
                principalTable: "ResourceRequest",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technology_ResourceRequest_ResourceRequestId",
                table: "Technology");

            migrationBuilder.DropTable(
                name: "TeamResource");

            migrationBuilder.DropTable(
                name: "ResourceRequest");

            migrationBuilder.DropIndex(
                name: "IX_Technology_ResourceRequestId",
                table: "Technology");

            migrationBuilder.DropColumn(
                name: "ResourceRequestId",
                table: "Technology");
        }
    }
}
