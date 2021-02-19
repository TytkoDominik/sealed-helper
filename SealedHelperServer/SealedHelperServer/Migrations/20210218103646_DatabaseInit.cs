using Microsoft.EntityFrameworkCore.Migrations;

namespace SealedHelperServer.Migrations
{
    public partial class DatabaseInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    DeckId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Expansion = table.Column<int>(type: "INTEGER", nullable: false),
                    House_0 = table.Column<int>(type: "INTEGER", nullable: false),
                    House_1 = table.Column<int>(type: "INTEGER", nullable: false),
                    House_2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Sas = table.Column<int>(type: "INTEGER", nullable: false),
                    Antisynergy = table.Column<int>(type: "INTEGER", nullable: false),
                    Synergy = table.Column<int>(type: "INTEGER", nullable: false),
                    Aerc = table.Column<int>(type: "INTEGER", nullable: false),
                    AemberControl = table.Column<float>(type: "REAL", nullable: false),
                    ExpectedAember = table.Column<float>(type: "REAL", nullable: false),
                    ArtifactControl = table.Column<float>(type: "REAL", nullable: false),
                    CreatureControl = table.Column<float>(type: "REAL", nullable: false),
                    Efficiency = table.Column<float>(type: "REAL", nullable: false),
                    Recursion = table.Column<float>(type: "REAL", nullable: false),
                    Disruption = table.Column<float>(type: "REAL", nullable: false),
                    CreatureProtection = table.Column<float>(type: "REAL", nullable: false),
                    Other = table.Column<float>(type: "REAL", nullable: false),
                    EffectivePower = table.Column<int>(type: "INTEGER", nullable: false),
                    RawAember = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionCount = table.Column<int>(type: "INTEGER", nullable: false),
                    UpgradeCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatureCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ArtifactCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Chains = table.Column<int>(type: "INTEGER", nullable: false),
                    Wins = table.Column<int>(type: "INTEGER", nullable: false),
                    Loses = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.DeckId);
                });

            migrationBuilder.CreateTable(
                name: "CardData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    DeckId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardData_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardData_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "DeckId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardData_CardId",
                table: "CardData",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardData_DeckId",
                table: "CardData",
                column: "DeckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardData");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Decks");
        }
    }
}
