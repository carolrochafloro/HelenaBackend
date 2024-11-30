using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatetBy",
                table: "Users",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdateAt",
                table: "Users",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatetBy",
                table: "Medications",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdateAt",
                table: "Medications",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatetBy",
                table: "Doctors",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdateAt",
                table: "Doctors",
                newName: "LastUpdatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Users",
                newName: "LastUpdatetBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "Users",
                newName: "LastUpdateAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Medications",
                newName: "LastUpdatetBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "Medications",
                newName: "LastUpdateAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Doctors",
                newName: "LastUpdatetBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "Doctors",
                newName: "LastUpdateAt");
        }
    }
}
