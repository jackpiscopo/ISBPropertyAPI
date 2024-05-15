using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddContactsAndOwnership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyPriceChange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    NewPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPriceChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyPriceChange_Properties_PropertyId1",
                        column: x => x.PropertyId1,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyOwnership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTill = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcquisitionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PropertyId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwnership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyOwnership_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyOwnership_Properties_PropertyId1",
                        column: x => x.PropertyId1,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnership_ContactId",
                table: "PropertyOwnership",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnership_PropertyId1",
                table: "PropertyOwnership",
                column: "PropertyId1");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPriceChange_PropertyId1",
                table: "PropertyPriceChange",
                column: "PropertyId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyOwnership");

            migrationBuilder.DropTable(
                name: "PropertyPriceChange");

            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
