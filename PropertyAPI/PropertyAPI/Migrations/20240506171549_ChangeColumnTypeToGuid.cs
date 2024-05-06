using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnTypeToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            // Add a temporary GUID column
            migrationBuilder.AddColumn<Guid>(
                name: "TempPropertyId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newsequentialid()");

            // Drop the old 'PropertyId' column
            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Properties");

            // Rename the 'TempPropertyId' column to 'PropertyId'
            migrationBuilder.RenameColumn(
                name: "TempPropertyId",
                table: "Properties",
                newName: "PropertyId");

            // Re-add the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Properties",
                newName: "TempPropertyId");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropColumn(
                name: "TempPropertyId",
                table: "Properties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyId");
        }
    }
}
