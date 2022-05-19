using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class ProjectAssessmentReportInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectAssessmentReportId",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectAssessmentReportId",
                table: "Approval",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectAssessmentReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnovationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssessmentReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectAssessmentReport_Innovation_InnovationId",
                        column: x => x.InnovationId,
                        principalTable: "Innovation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Filepath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Filesize = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectAssessmentReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_ProjectAssessmentReport_ProjectAssessmentReportId",
                        column: x => x.ProjectAssessmentReportId,
                        principalTable: "ProjectAssessmentReport",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProjectAssessmentReportId",
                table: "Comment",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ProjectAssessmentReportId",
                table: "Approval",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ProjectAssessmentReportId",
                table: "Attachment",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssessmentReport_InnovationId",
                table: "ProjectAssessmentReport",
                column: "InnovationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Approval_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "Approval",
                column: "ProjectAssessmentReportId",
                principalTable: "ProjectAssessmentReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "Comment",
                column: "ProjectAssessmentReportId",
                principalTable: "ProjectAssessmentReport",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approval_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "Approval");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "ProjectAssessmentReport");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ProjectAssessmentReportId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Approval_ProjectAssessmentReportId",
                table: "Approval");

            migrationBuilder.DropColumn(
                name: "ProjectAssessmentReportId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ProjectAssessmentReportId",
                table: "Approval");
        }
    }
}
