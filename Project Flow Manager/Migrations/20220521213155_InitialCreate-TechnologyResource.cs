using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class InitialCreateTechnologyResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technology_ResourceRequest_ResourceRequestId",
                table: "Technology");

            migrationBuilder.DropIndex(
                name: "IX_Technology_ResourceRequestId",
                table: "Technology");

            migrationBuilder.DropColumn(
                name: "ResourceRequestId",
                table: "Technology");

            migrationBuilder.CreateTable(
                name: "TechnologyResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnologyResource_ResourceRequest_ResourceRequestId",
                        column: x => x.ResourceRequestId,
                        principalTable: "ResourceRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyResource_ResourceRequestId",
                table: "TechnologyResource",
                column: "ResourceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnologyResource");

            migrationBuilder.AddColumn<int>(
                name: "ResourceRequestId",
                table: "Technology",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technology_ResourceRequestId",
                table: "Technology",
                column: "ResourceRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technology_ResourceRequest_ResourceRequestId",
                table: "Technology",
                column: "ResourceRequestId",
                principalTable: "ResourceRequest",
                principalColumn: "Id");
        }
    }
}
