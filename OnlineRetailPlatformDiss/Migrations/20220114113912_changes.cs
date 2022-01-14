using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineRetailPlatformDiss.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "BusinessAccount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserID",
                table: "BusinessAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
