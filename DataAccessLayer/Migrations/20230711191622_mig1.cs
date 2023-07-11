using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Galaxies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galaxies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxSpeed = table.Column<int>(type: "int", nullable: false),
                    SingleChargeRange = table.Column<int>(type: "int", nullable: false),
                    IfBroken = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalaxyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StarSystems_Galaxies_GalaxyId",
                        column: x => x.GalaxyId,
                        principalTable: "Galaxies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discoverers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discoverers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discoverers_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    StarSystemId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planets_StarSystems_StarSystemId",
                        column: x => x.StarSystemId,
                        principalTable: "StarSystems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Radius = table.Column<double>(type: "float", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Luminosity = table.Column<double>(type: "float", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    StarSystemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stars_StarSystems_StarSystemId",
                        column: x => x.StarSystemId,
                        principalTable: "StarSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscovererStarSystem",
                columns: table => new
                {
                    DiscoverersId = table.Column<int>(type: "int", nullable: false),
                    StarSystemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscovererStarSystem", x => new { x.DiscoverersId, x.StarSystemsId });
                    table.ForeignKey(
                        name: "FK_DiscovererStarSystem_Discoverers_DiscoverersId",
                        column: x => x.DiscoverersId,
                        principalTable: "Discoverers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscovererStarSystem_StarSystems_StarSystemsId",
                        column: x => x.StarSystemsId,
                        principalTable: "StarSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discoverers",
                columns: new[] { "Id", "Age", "Name", "ShipId", "Surname" },
                values: new object[,]
                {
                    { 2, 34, "Marek", null, "Markowski" },
                    { 3, 30, "Darek", null, "Darkowski" }
                });

            migrationBuilder.InsertData(
                table: "Galaxies",
                columns: new[] { "Id", "Mass", "Name", "Type" },
                values: new object[,]
                {
                    { 1, 10000000000000.0, "Droga Mleczna", 0 },
                    { 2, 10000000000000.0, "Messier 87", 1 },
                    { 3, 10000000000000000.0, "GAL-CLUS-022058s", 2 },
                    { 4, 10000000000.0, "Wielka Mgławica Magellana", 3 }
                });

            migrationBuilder.InsertData(
                table: "Ships",
                columns: new[] { "Id", "IfBroken", "MaxSpeed", "Name", "ShipModel", "SingleChargeRange" },
                values: new object[,]
                {
                    { 1, false, 10, "StarShip_1", "m0001", 12 },
                    { 2, false, 100, " StarShip_2", "m0002", 120 },
                    { 3, true, 1000, "StarShip_3", "m0003", 122 }
                });

            migrationBuilder.InsertData(
                table: "Discoverers",
                columns: new[] { "Id", "Age", "Name", "ShipId", "Surname" },
                values: new object[] { 1, 43, "Piotrek", 1, "Piotrowski" });

            migrationBuilder.InsertData(
                table: "StarSystems",
                columns: new[] { "Id", "GalaxyId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Wolarz" },
                    { 2, 2, "Orzel" },
                    { 3, 3, "Skorpion" },
                    { 4, 4, "Strzelec" }
                });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Id", "Mass", "Name", "StarSystemId", "Type" },
                values: new object[,]
                {
                    { 1, 2000000000000.0, "Jupiter", 1, 0 },
                    { 2, 2000000000000.0, "Neptune", 1, 0 },
                    { 3, 2000000000000.0, "Uranus", 1, 0 },
                    { 4, 2000000000000.0, "Saturn", 1, 0 },
                    { 5, 3000000000000.0, "Pluto", 1, 1 },
                    { 6, 4000000000000.0, "Kepler-438b", 2, 2 },
                    { 7, 5000000000000.0, "Earth", 1, 3 },
                    { 8, 5000000000000.0, "Mars", 1, 3 },
                    { 9, 6000000000000.0, "Charon", 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Stars",
                columns: new[] { "Id", "Age", "Luminosity", "Mass", "Name", "Radius", "StarSystemId", "Temperature", "Type" },
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
                name: "IX_DiscovererStarSystem_StarSystemsId",
                table: "DiscovererStarSystem",
                column: "StarSystemsId");

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
