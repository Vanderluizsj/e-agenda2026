using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBDespesas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBDespesas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataOcorrencia = table.Column<DateOnly>(type: "date", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormaPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDespesas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBTarefas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prioridade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateOnly>(type: "date", nullable: false),
                    DataConclusao = table.Column<DateOnly>(type: "date", nullable: true),
                    PercentualConcluido = table.Column<int>(type: "int", nullable: false),
                    Concluida = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTarefas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBDespesasCategorias",
                columns: table => new
                {
                    CategoriasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DespesasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDespesasCategorias", x => new { x.CategoriasId, x.DespesasId });
                    table.ForeignKey(
                        name: "FK_TBDespesasCategorias_TBCategorias_CategoriasId",
                        column: x => x.CategoriasId,
                        principalTable: "TBCategorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBDespesasCategorias_TBDespesas_DespesasId",
                        column: x => x.DespesasId,
                        principalTable: "TBDespesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBItensTarefa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TarefaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Concluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBItensTarefa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBItensTarefa_TBTarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "TBTarefas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBDespesasCategorias_DespesasId",
                table: "TBDespesasCategorias",
                column: "DespesasId");

            migrationBuilder.CreateIndex(
                name: "IX_TBItensTarefa_TarefaId",
                table: "TBItensTarefa",
                column: "TarefaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBDespesasCategorias");

            migrationBuilder.DropTable(
                name: "TBItensTarefa");

            migrationBuilder.DropTable(
                name: "TBDespesas");

            migrationBuilder.DropTable(
                name: "TBTarefas");
        }
    }
}
