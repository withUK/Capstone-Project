using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class UpdateResourceRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceRequest_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "ResourceRequest");

            migrationBuilder.DropColumn(
                name: "ProjectAssessmentId",
                table: "ResourceRequest");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectAssessmentReportId",
                table: "ResourceRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceRequest_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "ResourceRequest",
                column: "ProjectAssessmentReportId",
                principalTable: "ProjectAssessmentReport",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceRequest_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "ResourceRequest");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectAssessmentReportId",
                table: "ResourceRequest",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectAssessmentId",
                table: "ResourceRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceRequest_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "ResourceRequest",
                column: "ProjectAssessmentReportId",
                principalTable: "ProjectAssessmentReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
