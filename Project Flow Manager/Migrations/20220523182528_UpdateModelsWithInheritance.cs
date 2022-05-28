using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class UpdateModelsWithInheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Recommendation");

            migrationBuilder.AddColumn<int>(
                name: "ProjectAssessmentReportId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendationId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Recommendation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Recommendation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProjectAssessmentReport",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectAssessmentReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendationId",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InnovationId",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ProjectAssessmentReportId",
                table: "Tag",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_RecommendationId",
                table: "Tag",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_RecommendationId",
                table: "Comment",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_InnovationId",
                table: "Attachment",
                column: "InnovationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Innovation_InnovationId",
                table: "Attachment",
                column: "InnovationId",
                principalTable: "Innovation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Recommendation_RecommendationId",
                table: "Comment",
                column: "RecommendationId",
                principalTable: "Recommendation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "Tag",
                column: "ProjectAssessmentReportId",
                principalTable: "ProjectAssessmentReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Recommendation_RecommendationId",
                table: "Tag",
                column: "RecommendationId",
                principalTable: "Recommendation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Innovation_InnovationId",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Recommendation_RecommendationId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Recommendation_RecommendationId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_ProjectAssessmentReportId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_RecommendationId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Comment_RecommendationId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_InnovationId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "ProjectAssessmentReportId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "RecommendationId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Recommendation");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Recommendation");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProjectAssessmentReport");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectAssessmentReport");

            migrationBuilder.DropColumn(
                name: "RecommendationId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "InnovationId",
                table: "Attachment");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Recommendation",
                type: "datetime2",
                nullable: true);
        }
    }
}
