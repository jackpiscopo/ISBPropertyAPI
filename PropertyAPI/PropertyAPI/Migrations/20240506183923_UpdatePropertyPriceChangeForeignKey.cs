using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyPriceChangeForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyOwnership_Properties_PropertyId1",
                table: "PropertyOwnership");
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPriceChange_Properties_PropertyId1",
                table: "PropertyPriceChange");
            
            // Remove indexes
            migrationBuilder.DropIndex(
                name: "IX_PropertyPriceChange_PropertyId1",
                table: "PropertyPriceChange");
            migrationBuilder.DropIndex(
                name: "IX_PropertyOwnership_PropertyId1",
                table: "PropertyOwnership");
            
            // Add new columns with Guid type
            migrationBuilder.AddColumn<Guid>(
                name: "NewPropertyId",
                table: "PropertyPriceChange",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");
            migrationBuilder.AddColumn<Guid>(
                name: "NewPropertyId",
                table: "PropertyOwnership",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");
            
            // Drop old columns
            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "PropertyPriceChange");
            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "PropertyOwnership");
            
            // Rename new columns
            migrationBuilder.RenameColumn(
                name: "NewPropertyId",
                table: "PropertyPriceChange",
                newName: "PropertyId");
            migrationBuilder.RenameColumn(
                name: "NewPropertyId",
                table: "PropertyOwnership",
                newName: "PropertyId");
            
            // Re-add indexes
            migrationBuilder.CreateIndex(
                name: "IX_PropertyPriceChange_PropertyId",
                table: "PropertyPriceChange",
                column: "PropertyId");
            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnership_PropertyId",
                table: "PropertyOwnership",
                column: "PropertyId");
            
            // Re-add foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_PropertyOwnership_Properties_PropertyId",
                table: "PropertyOwnership",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPriceChange_Properties_PropertyId",
                table: "PropertyPriceChange",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyOwnership_Properties_PropertyId",
                table: "PropertyOwnership");
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPriceChange_Properties_PropertyId",
                table: "PropertyPriceChange");
            
            // Remove indexes
            migrationBuilder.DropIndex(
                name: "IX_PropertyPriceChange_PropertyId",
                table: "PropertyPriceChange");
            migrationBuilder.DropIndex(
                name: "IX_PropertyOwnership_PropertyId",
                table: "PropertyOwnership");
            
            // Rename columns back to original with new type
            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "PropertyPriceChange",
                newName: "NewPropertyId");
            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "PropertyOwnership",
                newName: "NewPropertyId");
            
            // Add old columns
            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "PropertyPriceChange",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "PropertyOwnership",
                type: "int",
                nullable: false,
                defaultValue: 0);
            
            // Drop new columns
            migrationBuilder.DropColumn(
                name: "NewPropertyId",
                table: "PropertyPriceChange");
            migrationBuilder.DropColumn(
                name: "NewPropertyId",
                table: "PropertyOwnership");
            
            // Re-add indexes
            migrationBuilder.CreateIndex(
                name: "IX_PropertyPriceChange_PropertyId",
                table: "PropertyPriceChange",
                column: "PropertyId");
            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnership_PropertyId",
                table: "PropertyOwnership",
                column: "PropertyId");
            
            // Re-add foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_PropertyOwnership_Properties_PropertyId",
                table: "PropertyOwnership",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPriceChange_Properties_PropertyId",
                table: "PropertyPriceChange",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
            onDelete: ReferentialAction.Cascade);
        }
    }
}
