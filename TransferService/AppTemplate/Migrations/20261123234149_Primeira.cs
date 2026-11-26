using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransferService.Migrations
{
    /// <inheritdoc />
    public partial class Primeira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    JogadorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClubeDestinoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataEfetivacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValorTransferencia = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transferencias");
        }
    }
}
