using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AG.Products.API.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(18)", nullable: false),
                    SupplierCode = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "varchar(100)", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ManufactureDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
