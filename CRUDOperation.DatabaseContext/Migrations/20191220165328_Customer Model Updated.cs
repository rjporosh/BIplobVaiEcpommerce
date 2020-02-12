using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDOperation.DatabaseContext.Migrations
{
    public partial class CustomerModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Phone",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
