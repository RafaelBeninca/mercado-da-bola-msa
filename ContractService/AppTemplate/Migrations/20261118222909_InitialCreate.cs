using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    JogadorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClubeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFimEstiamda = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataEncerramento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SalarioMes = table.Column<decimal>(type: "TEXT", nullable: false),
                    MultaRescisoria = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contratos");
        }
    }
}
