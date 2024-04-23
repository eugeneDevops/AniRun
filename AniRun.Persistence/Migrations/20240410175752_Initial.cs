using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniRun.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AniRun");

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "AniRun",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                schema: "AniRun",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studios",
                schema: "AniRun",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                schema: "AniRun",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    StartDateTitle = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndDateTitle = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PictureId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastEpisode = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    StudioId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Titles_Medias_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "AniRun",
                        principalTable: "Medias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Titles_Studios_StudioId",
                        column: x => x.StudioId,
                        principalSchema: "AniRun",
                        principalTable: "Studios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                schema: "AniRun",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    VideoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    TitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Medias_VideoId",
                        column: x => x.VideoId,
                        principalSchema: "AniRun",
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Episodes_Titles_TitleId",
                        column: x => x.TitleId,
                        principalSchema: "AniRun",
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreTitle",
                schema: "AniRun",
                columns: table => new
                {
                    GenresId = table.Column<Guid>(type: "uuid", nullable: false),
                    TitlesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreTitle", x => new { x.GenresId, x.TitlesId });
                    table.ForeignKey(
                        name: "FK_GenreTitle_Genres_GenresId",
                        column: x => x.GenresId,
                        principalSchema: "AniRun",
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreTitle_Titles_TitlesId",
                        column: x => x.TitlesId,
                        principalSchema: "AniRun",
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_TitleId",
                schema: "AniRun",
                table: "Episodes",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_VideoId",
                schema: "AniRun",
                table: "Episodes",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreTitle_TitlesId",
                schema: "AniRun",
                table: "GenreTitle",
                column: "TitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_PictureId",
                schema: "AniRun",
                table: "Titles",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_StudioId",
                schema: "AniRun",
                table: "Titles",
                column: "StudioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Episodes",
                schema: "AniRun");

            migrationBuilder.DropTable(
                name: "GenreTitle",
                schema: "AniRun");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "AniRun");

            migrationBuilder.DropTable(
                name: "Titles",
                schema: "AniRun");

            migrationBuilder.DropTable(
                name: "Medias",
                schema: "AniRun");

            migrationBuilder.DropTable(
                name: "Studios",
                schema: "AniRun");
        }
    }
}
