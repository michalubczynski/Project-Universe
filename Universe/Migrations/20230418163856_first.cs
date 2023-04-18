using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UI_Universe.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Galaxies",
                columns: table => new
                {
                    GalaxyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galaxies", x => x.GalaxyId);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ShipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxSpeed = table.Column<int>(type: "int", nullable: false),
                    SingleChargeRange = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ShipId);
                });

            migrationBuilder.CreateTable(
                name: "StarSystems",
                columns: table => new
                {
                    StarSystemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GalaxyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarSystems", x => x.StarSystemId);
                    table.ForeignKey(
                        name: "FK_StarSystems_Galaxies_GalaxyId",
                        column: x => x.GalaxyId,
                        principalTable: "Galaxies",
                        principalColumn: "GalaxyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discoverers",
                columns: table => new
                {
                    DiscovererId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discoverers", x => x.DiscovererId);
                    table.ForeignKey(
                        name: "FK_Discoverers_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "ShipId");
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    PlanetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    StarSystemId = table.Column<int>(type: "int", nullable: true),
                    GalaxyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.PlanetId);
                    table.ForeignKey(
                        name: "FK_Planets_Galaxies_GalaxyId",
                        column: x => x.GalaxyId,
                        principalTable: "Galaxies",
                        principalColumn: "GalaxyId");
                    table.ForeignKey(
                        name: "FK_Planets_StarSystems_StarSystemId",
                        column: x => x.StarSystemId,
                        principalTable: "StarSystems",
                        principalColumn: "StarSystemId");
                });

            migrationBuilder.CreateTable(
                name: "Stars",
                columns: table => new
                {
                    StarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Radius = table.Column<double>(type: "float", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Luminosity = table.Column<double>(type: "float", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    StarSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stars", x => x.StarId);
                    table.ForeignKey(
                        name: "FK_Stars_StarSystems_StarSystemId",
                        column: x => x.StarSystemId,
                        principalTable: "StarSystems",
                        principalColumn: "StarSystemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscovererStarSystem",
                columns: table => new
                {
                    DiscoverersDiscovererId = table.Column<int>(type: "int", nullable: false),
                    StarSystemsStarSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscovererStarSystem", x => new { x.DiscoverersDiscovererId, x.StarSystemsStarSystemId });
                    table.ForeignKey(
                        name: "FK_DiscovererStarSystem_Discoverers_DiscoverersDiscovererId",
                        column: x => x.DiscoverersDiscovererId,
                        principalTable: "Discoverers",
                        principalColumn: "DiscovererId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscovererStarSystem_StarSystems_StarSystemsStarSystemId",
                        column: x => x.StarSystemsStarSystemId,
                        principalTable: "StarSystems",
                        principalColumn: "StarSystemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discoverers",
                columns: new[] { "DiscovererId", "Age", "Name", "ShipId", "Surname" },
                values: new object[,]
                {
                    { 2, 34, "Marek", null, "Markowski" },
                    { 3, 30, "Darek", null, "Darkowski" }
                });

            migrationBuilder.InsertData(
                table: "Galaxies",
                columns: new[] { "GalaxyId", "Mass", "Name", "Type" },
                values: new object[,]
                {
                    { 1, 10000000000000.0, "Droga Mleczna", 0 },
                    { 2, 10000000000000.0, "Messier 87", 1 },
                    { 3, 10000000000000000.0, "GAL-CLUS-022058s", 2 },
                    { 4, 10000000000.0, "Wielka Mgławica Magellana", 3 }
                });

            migrationBuilder.InsertData(
                table: "Ships",
                columns: new[] { "ShipId", "MaxSpeed", "ShipModel", "ShipName", "SingleChargeRange" },
                values: new object[,]
                {
                    { 1, 10, "Super", "Mewa", 12 },
                    { 2, 100, "Duper", " Jaszczomp", 120 },
                    { 3, 1000, "DuperSuper", "Orzel", 122 }
                });

            migrationBuilder.InsertData(
                table: "Discoverers",
                columns: new[] { "DiscovererId", "Age", "Name", "ShipId", "Surname" },
                values: new object[] { 1, 43, "Piotrek", 1, "Piotrowski" });

            migrationBuilder.InsertData(
                table: "StarSystems",
                columns: new[] { "StarSystemId", "GalaxyId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Wolarz" },
                    { 2, 2, "Orzel" },
                    { 3, 3, "Skorpion" },
                    { 4, 4, "Strzelec" }
                });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "PlanetId", "GalaxyId", "Mass", "Name", "StarSystemId", "Type" },
                values: new object[,]
                {
                    { 1, null, 2000000000000.0, "Jupiter", 1, 0 },
                    { 2, null, 2000000000000.0, "Neptune", 1, 0 },
                    { 3, null, 2000000000000.0, "Uranus", 1, 0 },
                    { 4, null, 2000000000000.0, "Saturn", 1, 0 },
                    { 5, null, 3000000000000.0, "Pluto", 1, 1 },
                    { 6, null, 4000000000000.0, "Kepler-438b", 2, 2 },
                    { 7, null, 5000000000000.0, "Earth", 1, 3 },
                    { 8, null, 5000000000000.0, "Mars", 1, 3 },
                    { 9, null, 6000000000000.0, "Charon", 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Stars",
                columns: new[] { "StarId", "Age", "Luminosity", "Mass", "Name", "Radius", "StarSystemId", "Temperature", "Type" },
                values: new object[,]
                {
                    { 1, 10000, 23.0, 1000000.0, "Zeta", 22.100000000000001, 1, 30.0, 0 },
                    { 2, 1000, 233.0, 100000.0, "Aldebaran", 232.09999999999999, 1, 303.0, 1 },
                    { 3, 10000, 3.0, 1000000.0, "SiriusB", 322.10000000000002, 1, 130.0, 2 },
                    { 4, 100000, 235.0, 100000000.0, "PSR_B1509-58", 2.1000000000000001, 3, 0.0, 3 },
                    { 5, 100000, 0.0, 100000000.0, "Cygnus X-1", 33334.099999999999, 2, -22.0, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discoverers_ShipId",
                table: "Discoverers",
                column: "ShipId",
                unique: true,
                filter: "[ShipId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DiscovererStarSystem_StarSystemsStarSystemId",
                table: "DiscovererStarSystem",
                column: "StarSystemsStarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_GalaxyId",
                table: "Planets",
                column: "GalaxyId");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_StarSystemId",
                table: "Planets",
                column: "StarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_StarSystemId",
                table: "Stars",
                column: "StarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_StarSystems_GalaxyId",
                table: "StarSystems",
                column: "GalaxyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscovererStarSystem");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "Stars");

            migrationBuilder.DropTable(
                name: "Discoverers");

            migrationBuilder.DropTable(
                name: "StarSystems");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Galaxies");
        }
    }
}
