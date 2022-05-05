using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class UpdateInnovationModel_AddApprovalNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innovation_Approval_ApprovalId",
                table: "Innovation");

            migrationBuilder.AlterColumn<int>(
                name: "ApprovalId",
                table: "Innovation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Innovation_Approval_ApprovalId",
                table: "Innovation",
                column: "ApprovalId",
                principalTable: "Approval",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innovation_Approval_ApprovalId",
                table: "Innovation");

            migrationBuilder.AlterColumn<int>(
                name: "ApprovalId",
                table: "Innovation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Innovation_Approval_ApprovalId",
                table: "Innovation",
                column: "ApprovalId",
                principalTable: "Approval",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
