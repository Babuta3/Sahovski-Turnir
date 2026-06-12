using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlojPodataka.Migrations
{
    /// <inheritdoc />
    public partial class CelaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Igrac",
                columns: table => new
                {
                    IgracID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Klub = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Titula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ELO = table.Column<int>(type: "int", nullable: true),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igrac", x => x.IgracID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LozinkaHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "Turnir",
                columns: table => new
                {
                    TurnirID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTurnira = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mesto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Organizator = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GlavniArbitar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NagradniFond = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TipTurnira = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormatTakmicenja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrojRundi = table.Column<int>(type: "int", nullable: false),
                    BrojUcesnika = table.Column<int>(type: "int", nullable: false),
                    VremenskaKontrola = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TiebreakKriterijum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnir", x => x.TurnirID);
                });

            migrationBuilder.CreateTable(
                name: "PlasmanIgraca",
                columns: table => new
                {
                    PlasmanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnirID = table.Column<int>(type: "int", nullable: false),
                    IgracID = table.Column<int>(type: "int", nullable: false),
                    Mesto = table.Column<int>(type: "int", nullable: false),
                    Bodovi = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Nagrada = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlasmanIgraca", x => x.PlasmanID);
                    table.ForeignKey(
                        name: "FK_PlasmanIgraca_Igrac_IgracID",
                        column: x => x.IgracID,
                        principalTable: "Igrac",
                        principalColumn: "IgracID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlasmanIgraca_Turnir_TurnirID",
                        column: x => x.TurnirID,
                        principalTable: "Turnir",
                        principalColumn: "TurnirID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlasmanIgraca_IgracID",
                table: "PlasmanIgraca",
                column: "IgracID");

            migrationBuilder.CreateIndex(
                name: "IX_PlasmanIgraca_TurnirID",
                table: "PlasmanIgraca",
                column: "TurnirID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "PlasmanIgraca");

            migrationBuilder.DropTable(
                name: "Igrac");

            migrationBuilder.DropTable(
                name: "Turnir");
        }
    }
}
