using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INDEShipping.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostalCodeAndDecimalTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodes_TransportCompanies_TransportCompanyId",
                table: "PostalCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodes_TransportCompanies_TransportCompanyId1",
                table: "PostalCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodes_TransportCompanies_TransportCompanyId2",
                table: "PostalCodes");

            migrationBuilder.DropIndex(
                name: "IX_PostalCodes_TransportCompanyId1",
                table: "PostalCodes");

            migrationBuilder.DropIndex(
                name: "IX_PostalCodes_TransportCompanyId2",
                table: "PostalCodes");

            migrationBuilder.DropColumn(
                name: "TransportCompanyId1",
                table: "PostalCodes");

            migrationBuilder.DropColumn(
                name: "TransportCompanyId2",
                table: "PostalCodes");

            migrationBuilder.AlterColumn<int>(
                name: "TransportCompanyId",
                table: "PostalCodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodes_TransportCompanies_TransportCompanyId",
                table: "PostalCodes",
                column: "TransportCompanyId",
                principalTable: "TransportCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodes_TransportCompanies_TransportCompanyId",
                table: "PostalCodes");

            migrationBuilder.AlterColumn<int>(
                name: "TransportCompanyId",
                table: "PostalCodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TransportCompanyId1",
                table: "PostalCodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportCompanyId2",
                table: "PostalCodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_TransportCompanyId1",
                table: "PostalCodes",
                column: "TransportCompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_TransportCompanyId2",
                table: "PostalCodes",
                column: "TransportCompanyId2");

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodes_TransportCompanies_TransportCompanyId",
                table: "PostalCodes",
                column: "TransportCompanyId",
                principalTable: "TransportCompanies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodes_TransportCompanies_TransportCompanyId1",
                table: "PostalCodes",
                column: "TransportCompanyId1",
                principalTable: "TransportCompanies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodes_TransportCompanies_TransportCompanyId2",
                table: "PostalCodes",
                column: "TransportCompanyId2",
                principalTable: "TransportCompanies",
                principalColumn: "Id");
        }
    }
}
