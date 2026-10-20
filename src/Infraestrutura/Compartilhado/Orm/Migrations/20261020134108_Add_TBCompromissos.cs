using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBCompromissos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCompromissos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Assunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataDeOcorrencia = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraDeInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraDeTermino = table.Column<TimeOnly>(type: "time", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCompromissos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBCompromissos_TBContatos_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "TBContatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBCompromissos_ContatoId",
                table: "TBCompromissos",
                column: "ContatoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBCompromissos");
        }
    }
}
