using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovePropertyId1Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "PropertyId1",
            table: "PropertyOwnership");

            migrationBuilder.DropColumn(
                name: "PropertyId1",
                table: "PropertyPriceChange");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
            name: "PropertyId1",
            table: "PropertyOwnership",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid());

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId1",
                table: "PropertyPriceChange",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid());
        }
    }
}
