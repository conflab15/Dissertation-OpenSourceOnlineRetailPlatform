using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineRetailPlatformDiss.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialMediaLink",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    BusinessAccountModelId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaLink", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SocialMediaLink_BusinessAccount_BusinessAccountModelId",
                        column: x => x.BusinessAccountModelId,
                        principalTable: "BusinessAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaLink_BusinessAccountModelId",
                table: "SocialMediaLink",
                column: "BusinessAccountModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMediaLink");
        }
    }
}
