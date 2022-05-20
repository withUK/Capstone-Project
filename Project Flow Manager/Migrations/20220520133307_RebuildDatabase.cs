using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class RebuildDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Effort",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    Measure = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Effort", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectAssessmentReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Innovation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessDuration = table.Column<int>(type: "int", nullable: false),
                    NumberOfPeopleIncluded = table.Column<int>(type: "int", nullable: false),
                    ProcessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Innovation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Innovation_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectAssessmentReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InnovationId = table.Column<int>(type: "int", nullable: false),
                    ChosenRecommendationId = table.Column<int>(type: "int", nullable: true)
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
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InnovationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Innovation_InnovationId",
                        column: x => x.InnovationId,
                        principalTable: "Innovation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InnovationId = table.Column<int>(type: "int", nullable: true),
                    ProjectAssessmentReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Innovation_InnovationId",
                        column: x => x.InnovationId,
                        principalTable: "Innovation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_ProjectAssessmentReport_ProjectAssessmentReportId",
                        column: x => x.ProjectAssessmentReportId,
                        principalTable: "ProjectAssessmentReport",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recommendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffortId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectAssessmentReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendation_Effort_EffortId",
                        column: x => x.EffortId,
                        principalTable: "Effort",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recommendation_ProjectAssessmentReport_ProjectAssessmentReportId",
                        column: x => x.ProjectAssessmentReportId,
                        principalTable: "ProjectAssessmentReport",
                        principalColumn: "Id");
                });

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
                    ProjectAssessmentReportId = table.Column<int>(type: "int", nullable: true),
                    RecommendationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_ProjectAssessmentReport_ProjectAssessmentReportId",
                        column: x => x.ProjectAssessmentReportId,
                        principalTable: "ProjectAssessmentReport",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_Recommendation_RecommendationId",
                        column: x => x.RecommendationId,
                        principalTable: "Recommendation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessStep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderPosition = table.Column<int>(type: "int", nullable: false),
                    InnovationId = table.Column<int>(type: "int", nullable: true),
                    RecommendationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessStep_Innovation_InnovationId",
                        column: x => x.InnovationId,
                        principalTable: "Innovation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessStep_Recommendation_RecommendationId",
                        column: x => x.RecommendationId,
                        principalTable: "Recommendation",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "Technology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InnovationId = table.Column<int>(type: "int", nullable: true),
                    RecommendationId = table.Column<int>(type: "int", nullable: true),
                    ResourceRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technology_Innovation_InnovationId",
                        column: x => x.InnovationId,
                        principalTable: "Innovation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Technology_Recommendation_RecommendationId",
                        column: x => x.RecommendationId,
                        principalTable: "Recommendation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Technology_ResourceRequest_ResourceRequestId",
                        column: x => x.ResourceRequestId,
                        principalTable: "ResourceRequest",
                        principalColumn: "Id");
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
                name: "IX_Approval_ProjectAssessmentReportId",
                table: "Approval",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ProjectAssessmentReportId",
                table: "Attachment",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_RecommendationId",
                table: "Attachment",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_InnovationId",
                table: "Comment",
                column: "InnovationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProjectAssessmentReportId",
                table: "Comment",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Innovation_ApprovalId",
                table: "Innovation",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStep_InnovationId",
                table: "ProcessStep",
                column: "InnovationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStep_RecommendationId",
                table: "ProcessStep",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssessmentReport_InnovationId",
                table: "ProjectAssessmentReport",
                column: "InnovationId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_EffortId",
                table: "Recommendation",
                column: "EffortId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_ProjectAssessmentReportId",
                table: "Recommendation",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRequest_ProjectAssessmentReportId",
                table: "ResourceRequest",
                column: "ProjectAssessmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_InnovationId",
                table: "Tag",
                column: "InnovationId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_RecommendationId",
                table: "Team",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamResource_ResourceRequestId",
                table: "TeamResource",
                column: "ResourceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamResource_TeamId",
                table: "TeamResource",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Technology_InnovationId",
                table: "Technology",
                column: "InnovationId");

            migrationBuilder.CreateIndex(
                name: "IX_Technology_RecommendationId",
                table: "Technology",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_Technology_ResourceRequestId",
                table: "Technology",
                column: "ResourceRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Approval_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "Approval",
                column: "ProjectAssessmentReportId",
                principalTable: "ProjectAssessmentReport",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approval_ProjectAssessmentReport_ProjectAssessmentReportId",
                table: "Approval");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ProcessStep");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "TeamResource");

            migrationBuilder.DropTable(
                name: "Technology");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "ResourceRequest");

            migrationBuilder.DropTable(
                name: "Recommendation");

            migrationBuilder.DropTable(
                name: "Effort");

            migrationBuilder.DropTable(
                name: "ProjectAssessmentReport");

            migrationBuilder.DropTable(
                name: "Innovation");

            migrationBuilder.DropTable(
                name: "Approval");
        }
    }
}
