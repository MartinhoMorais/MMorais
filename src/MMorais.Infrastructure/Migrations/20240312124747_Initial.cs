using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMorais.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(14)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DataCadastro",
                table: "Usuario",
                column: "DataCadastro");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Nome",
                table: "Usuario",
                column: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
