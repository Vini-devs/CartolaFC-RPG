using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartolaFCRPG.Migrations
{
    /// <inheritdoc />
    public partial class Simplificacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_PosicaoCampo_PosicaoId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Times_TimeId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Times_TimeCasaId",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Times_TimeForaId",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Torneios_TorneioId",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Torneios_TorneioId",
                table: "Times");

            migrationBuilder.DropTable(
                name: "PosicaoCampo");

            migrationBuilder.DropIndex(
                name: "IX_Times_TorneioId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_TimeCasaId",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_TimeForaId",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_TorneioId",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_PosicaoId",
                table: "Jogadores");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_TimeId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Torneios");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Torneios");

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
                name: "Finalizada",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "TimeCasaId",
                table: "Partidas");

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

            migrationBuilder.RenameColumn(
                name: "UrlEscudo",
                table: "Times",
                newName: "JogadorIds");

            migrationBuilder.RenameColumn(
                name: "TorneioId",
                table: "Partidas",
                newName: "TimeIdFora");

            migrationBuilder.RenameColumn(
                name: "TimeForaId",
                table: "Partidas",
                newName: "TimeIdCasa");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Torneios",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "PartidaIds",
                table: "Torneios",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AlterColumn<string>(
                name: "Sigla",
                table: "Times",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Times",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Posicao",
                table: "Jogadores",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartidaIds",
                table: "Torneios");

            migrationBuilder.DropColumn(
                name: "Posicao",
                table: "Jogadores");

            migrationBuilder.RenameColumn(
                name: "JogadorIds",
                table: "Times",
                newName: "UrlEscudo");

            migrationBuilder.RenameColumn(
                name: "TimeIdFora",
                table: "Partidas",
                newName: "TorneioId");

            migrationBuilder.RenameColumn(
                name: "TimeIdCasa",
                table: "Partidas",
                newName: "TimeForaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Torneios",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Torneios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Torneios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Sigla",
                table: "Times",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Times",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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

            migrationBuilder.AddColumn<bool>(
                name: "Finalizada",
                table: "Partidas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TimeCasaId",
                table: "Partidas",
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

            migrationBuilder.CreateIndex(
                name: "IX_Times_TorneioId",
                table: "Times",
                column: "TorneioId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_PosicaoId",
                table: "Jogadores",
                column: "PosicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_TimeId",
                table: "Jogadores",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_PosicaoCampo_PosicaoId",
                table: "Jogadores",
                column: "PosicaoId",
                principalTable: "PosicaoCampo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Times_TimeId",
                table: "Jogadores",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Times_TimeCasaId",
                table: "Partidas",
                column: "TimeCasaId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Times_TimeForaId",
                table: "Partidas",
                column: "TimeForaId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Torneios_TorneioId",
                table: "Partidas",
                column: "TorneioId",
                principalTable: "Torneios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Torneios_TorneioId",
                table: "Times",
                column: "TorneioId",
                principalTable: "Torneios",
                principalColumn: "Id");
        }
    }
}
