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
    [Migration("20220502202000_UpdateInnovationModel_AddApproval")]
    partial class UpdateInnovationModel_AddApproval
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project_Flow_Manager_Models.Innovation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApprovalId")
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

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InnovationId");

                    b.ToTable("ProcessStep");
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

                    b.HasKey("Id");

                    b.HasIndex("InnovationId");

                    b.ToTable("Technology");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.Approval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApprovedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ApprovedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Outcome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Approval");
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

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InnovationId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Innovation", b =>
                {
                    b.HasOne("ProjectFlowManagerModels.Approval", "Approval")
                        .WithMany()
                        .HasForeignKey("ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.ProcessStep", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", null)
                        .WithMany("ProcessSteps")
                        .HasForeignKey("InnovationId");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Tag", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", null)
                        .WithMany("Tags")
                        .HasForeignKey("InnovationId");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Technology", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", null)
                        .WithMany("Technologies")
                        .HasForeignKey("InnovationId");
                });

            modelBuilder.Entity("ProjectFlowManagerModels.Comment", b =>
                {
                    b.HasOne("Project_Flow_Manager_Models.Innovation", null)
                        .WithMany("Comments")
                        .HasForeignKey("InnovationId");
                });

            modelBuilder.Entity("Project_Flow_Manager_Models.Innovation", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ProcessSteps");

                    b.Navigation("Tags");

                    b.Navigation("Technologies");
                });
#pragma warning restore 612, 618
        }
    }
}