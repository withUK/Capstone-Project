﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    [DbContext(typeof(InnovationManagerContext))]
    [Migration("20220516193009_ProjectAssessmentReport-AddedChosenRecomendation")]
    partial class ProjectAssessmentReportAddedChosenRecomendation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project_Flow_Manager_Models.Effort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Measure")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Effort");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Innovation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ApprovalId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPeopleIncluded")
                        .HasColumnType("int");

                    b.Property<int>("ProcessDuration")
                        .HasColumnType("int");

                    b.Property<string>("ProcessType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequiredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubmittedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmittedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalId");

                    b.ToTable("Innovation");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.ProcessStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("InnovationId")
                        .HasColumnType("int");

                    b.Property<int>("OrderPosition")
                        .HasColumnType("int");

                    b.Property<int?>("RecommendationId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InnovationId");

                    b.HasIndex("RecommendationId");

                    b.ToTable("ProcessStep");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Recommendation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EffortId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectAssessmentReportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EffortId");

                    b.HasIndex("ProjectAssessmentReportId");

                    b.ToTable("Recommendation");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("InnovationId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InnovationId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecommendationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecommendationId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("InnovationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecommendationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InnovationId");

                    b.HasIndex("RecommendationId");

                    b.ToTable("Technology");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.Approval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ApprovedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Outcome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectAssessmentReportId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectAssessmentReportId");

                    b.ToTable("Approval");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Filepath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Filesize")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectAssessmentReportId")
                        .HasColumnType("int");

                    b.Property<int?>("RecommendationId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectAssessmentReportId");

                    b.HasIndex("RecommendationId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InnovationId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectAssessmentReportId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InnovationId");

                    b.HasIndex("ProjectAssessmentReportId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.ProjectAssessmentReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ChosenRecommendationId")
                        .HasColumnType("int");

                    b.Property<int>("InnovationId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InnovationId");

                    b.ToTable("ProjectAssessmentReport");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Innovation", b =>
                {
                    b.HasOne("ProjectFlowManagerModels.Approval", "Approval")
                        .WithMany()
                        .HasForeignKey("ApprovalId");

                    b.Navigation("Approval");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.ProcessStep", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", null)
                        .WithMany("ProcessSteps")
                        .HasForeignKey("InnovationId");

                    b.HasOne("Project_Flow_Manager_Models.Recommendation", null)
                        .WithMany("ProcessSteps")
                        .HasForeignKey("RecommendationId");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Recommendation", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Effort", "Effort")
                        .WithMany()
                        .HasForeignKey("EffortId");

                    b.HasOne("ProjectFlowManagerModels.ProjectAssessmentReport", "ProjectAssessmentReport")
                        .WithMany("Recommendations")
                        .HasForeignKey("ProjectAssessmentReportId");

                    b.Navigation("Effort");

                    b.Navigation("ProjectAssessmentReport");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Tag", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", null)
                        .WithMany("Tags")
                        .HasForeignKey("InnovationId");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Team", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Recommendation", null)
                        .WithMany("Teams")
                        .HasForeignKey("RecommendationId");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Technology", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", null)
                        .WithMany("Technologies")
                        .HasForeignKey("InnovationId");

                    b.HasOne("Project_Flow_Manager_Models.Recommendation", null)
                        .WithMany("Technologies")
                        .HasForeignKey("RecommendationId");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.Approval", b =>
                {
                    b.HasOne("ProjectFlowManagerModels.ProjectAssessmentReport", null)
                        .WithMany("Approvals")
                        .HasForeignKey("ProjectAssessmentReportId");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.Attachment", b =>
                {
                    b.HasOne("ProjectFlowManagerModels.ProjectAssessmentReport", null)
                        .WithMany("Attachments")
                        .HasForeignKey("ProjectAssessmentReportId");

                    b.HasOne("Project_Flow_Manager_Models.Recommendation", null)
                        .WithMany("Attachments")
                        .HasForeignKey("RecommendationId");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.Comment", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", null)
                        .WithMany("Comments")
                        .HasForeignKey("InnovationId");

                    b.HasOne("ProjectFlowManagerModels.ProjectAssessmentReport", null)
                        .WithMany("Comments")
                        .HasForeignKey("ProjectAssessmentReportId");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.ProjectAssessmentReport", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", "Innovation")
                        .WithMany()
                        .HasForeignKey("InnovationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Innovation");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Innovation", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ProcessSteps");

                    b.Navigation("Tags");

                    b.Navigation("Technologies");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Recommendation", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("ProcessSteps");

                    b.Navigation("Teams");

                    b.Navigation("Technologies");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.ProjectAssessmentReport", b =>
                {
                    b.Navigation("Approvals");

                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("Recommendations");
                });
#pragma warning restore 612, 618
        }
    }
}
