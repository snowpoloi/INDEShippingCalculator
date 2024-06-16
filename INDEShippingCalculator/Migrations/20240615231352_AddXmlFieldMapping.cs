using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INDEShipping.Migrations
{
    /// <inheritdoc />
    public partial class AddXmlFieldMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxCubic = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XmlFieldMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XmlField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatabaseField = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XmlFieldMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportCompanyId = table.Column<int>(type: "int", nullable: false),
                    MinWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BaseCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExtraCostPerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExtraCostDifficult = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CubicRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_TransportCompanies_TransportCompanyId",
                        column: x => x.TransportCompanyId,
                        principalTable: "TransportCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nomos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDifficultAccess = table.Column<bool>(type: "bit", nullable: false),
                    NoCOD = table.Column<bool>(type: "bit", nullable: false),
                    TransportCompanyId = table.Column<int>(type: "int", nullable: true),
                    TransportCompanyId1 = table.Column<int>(type: "int", nullable: true),
                    TransportCompanyId2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostalCodes_TransportCompanies_TransportCompanyId",
                        column: x => x.TransportCompanyId,
                        principalTable: "TransportCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostalCodes_TransportCompanies_TransportCompanyId1",
                        column: x => x.TransportCompanyId1,
                        principalTable: "TransportCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostalCodes_TransportCompanies_TransportCompanyId2",
                        column: x => x.TransportCompanyId2,
                        principalTable: "TransportCompanies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_TransportCompanyId",
                table: "Offers",
                column: "TransportCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_TransportCompanyId",
                table: "PostalCodes",
                column: "TransportCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_TransportCompanyId1",
                table: "PostalCodes",
                column: "TransportCompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_TransportCompanyId2",
                table: "PostalCodes",
                column: "TransportCompanyId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "PostalCodes");

            migrationBuilder.DropTable(
                name: "XmlFieldMappings");

            migrationBuilder.DropTable(
                name: "TransportCompanies");
        }
    }
}
