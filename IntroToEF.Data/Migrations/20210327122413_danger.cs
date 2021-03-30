using Microsoft.EntityFrameworkCore.Migrations;

namespace IntroToEF.Data.Migrations
    {
    public partial class Danger : Migration
        {
        protected override void Up(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.CreateTable(
                name: "BattleSamurais",
                columns: table => new
                    {
                    SamuraisId = table.Column<int>(type: "int", nullable: false),
                    BattlesId = table.Column<int>(type: "int", nullable: false),
                    SamuraiId = table.Column<int>(type: "int", nullable: true),
                    BattleId = table.Column<int>(type: "int", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleSamurais", x => new { x.SamuraisId, x.BattlesId });
                    table.ForeignKey(
                        name: "FK_BattleSamurais_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BattleSamurais_Samurais_SamuraiId",
                        column: x => x.SamuraiId,
                        principalTable: "Samurais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleSamurais_BattleId",
                table: "BattleSamurais",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleSamurais_SamuraiId",
                table: "BattleSamurais",
                column: "SamuraiId");
            }

        protected override void Down(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.DropTable(
                name: "BattleSamurais");
            }
        }
    }