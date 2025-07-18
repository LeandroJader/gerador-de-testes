using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorDeTestes.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBMateria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Disciplina = table.Column<string>(type: "text", nullable: false),
                    Serie = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materia");
        }
    }
}
