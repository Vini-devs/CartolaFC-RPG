using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartolaFCRPG.Migrations
{
    /// <inheritdoc />
    public partial class PartidasTorneios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Posicao",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Jogadores");

            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "Times",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Empates",
                table: "Times",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GolsFeitos",
                table: "Times",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GolsSofridos",
                table: "Times",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TorneioId",
                table: "Times",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Vitorias",
                table: "Times",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Agilidade",
                table: "Jogadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AptidaoFisica",
                table: "Jogadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defesa",
                table: "Jogadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mentalidade",
                table: "Jogadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PosicaoId",
                table: "Jogadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrecisaoFinalizacao",
                table: "Jogadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrecisaoPasse",
                table: "Jogadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tecnica",
                table: "Jogadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PosicaoCampo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosicaoCampo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Torneios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeCasaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeForaId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlacarCasa = table.Column<int>(type: "INTEGER", nullable: false),
                    PlacarFora = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Finalizada = table.Column<bool>(type: "INTEGER", nullable: false),
                    TorneioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidas_Times_TimeCasaId",
                        column: x => x.TimeCasaId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partidas_Times_TimeForaId",
                        column: x => x.TimeForaId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partidas_Torneios_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Times_TorneioId",
                table: "Times",
                column: "TorneioId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_PosicaoId",
                table: "Jogadores",
                column: "PosicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_TimeCasaId",
                table: "Partidas",
                column: "TimeCasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_TimeForaId",
                table: "Partidas",
                column: "TimeForaId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_TorneioId",
                table: "Partidas",
                column: "TorneioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_PosicaoCampo_PosicaoId",
                table: "Jogadores",
                column: "PosicaoId",
                principalTable: "PosicaoCampo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Torneios_TorneioId",
                table: "Times",
                column: "TorneioId",
                principalTable: "Torneios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_PosicaoCampo_PosicaoId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Torneios_TorneioId",
                table: "Times");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "PosicaoCampo");

            migrationBuilder.DropTable(
                name: "Torneios");

            migrationBuilder.DropIndex(
                name: "IX_Times_TorneioId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_PosicaoId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Empates",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "GolsFeitos",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "GolsSofridos",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "TorneioId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Vitorias",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Agilidade",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "AptidaoFisica",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Defesa",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Mentalidade",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "PosicaoId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "PrecisaoFinalizacao",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "PrecisaoPasse",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Tecnica",
                table: "Jogadores");

            migrationBuilder.AddColumn<string>(
                name: "Posicao",
                table: "Jogadores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Jogadores",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
