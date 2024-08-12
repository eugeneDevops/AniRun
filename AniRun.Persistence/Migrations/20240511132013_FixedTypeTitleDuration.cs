using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniRun.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedTypeTitleDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Duration",
                schema: "AniRun",
                table: "Titles",
                type: "time without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Duration",
                schema: "AniRun",
                table: "Titles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true);
        }
    }
}
