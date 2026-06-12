using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlojPodataka.Migrations
{
    /// <inheritdoc />
    public partial class DodateStoređProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_DajUkupanBrojTurnira
                AS
                BEGIN
                    SELECT COUNT(*) FROM Turnir
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_DajUkupanBrojIgraca
                AS
                BEGIN
                    SELECT COUNT(*) FROM Igrac
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DajUkupanBrojTurnira");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DajUkupanBrojIgraca");
        }
    }
}
