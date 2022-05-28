using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class ModelUpdateResourceRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResourceRequestId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ResourceRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ResourceRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceRequestId",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceRequestId",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ResourceRequestId",
                table: "Tag",
                column: "ResourceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ResourceRequestId",
                table: "Comment",
                column: "ResourceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ResourceRequestId",
                table: "Attachment",
                column: "ResourceRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_ResourceRequest_ResourceRequestId",
                table: "Attachment",
                column: "ResourceRequestId",
                principalTable: "ResourceRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ResourceRequest_ResourceRequestId",
                table: "Comment",
                column: "ResourceRequestId",
                principalTable: "ResourceRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_ResourceRequest_ResourceRequestId",
                table: "Tag",
                column: "ResourceRequestId",
                principalTable: "ResourceRequest",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_ResourceRequest_ResourceRequestId",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ResourceRequest_ResourceRequestId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_ResourceRequest_ResourceRequestId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_ResourceRequestId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ResourceRequestId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_ResourceRequestId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "ResourceRequestId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ResourceRequest");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ResourceRequest");

            migrationBuilder.DropColumn(
                name: "ResourceRequestId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ResourceRequestId",
                table: "Attachment");
        }
    }
}
