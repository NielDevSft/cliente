using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "Cliente",
                schema: "app",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeComleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtaNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValRenda = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioUuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Uuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente",
                schema: "app");
        }
    }
}
