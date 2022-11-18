using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMvc.Migrations
{
	public partial class Products : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
					name: "Id",
					table: "Products",
					newName: "ProductId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
					name: "ProductId",
					table: "Products",
					newName: "Id");
		}
	}
}
