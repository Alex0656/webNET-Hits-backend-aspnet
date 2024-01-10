using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab1_backend.Migrations
{
    public partial class firstname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteMovies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    MovieId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMovies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserShortModel",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    NickName = table.Column<string>(type: "TEXT", nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShortModel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    ReviewText = table.Column<string>(type: "TEXT", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "INTEGER", nullable: false),
                    createDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Authorid = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewModel_UserShortModel_Authorid",
                        column: x => x.Authorid,
                        principalTable: "UserShortModel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    poster = table.Column<string>(type: "TEXT", nullable: false),
                    year = table.Column<int>(type: "INTEGER", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    ReviewsId = table.Column<string>(type: "TEXT", nullable: true),
                    time = table.Column<int>(type: "INTEGER", nullable: false),
                    tagline = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    director = table.Column<string>(type: "TEXT", nullable: false),
                    budget = table.Column<int>(type: "INTEGER", nullable: false),
                    fees = table.Column<int>(type: "INTEGER", nullable: false),
                    ageLimit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_ReviewModel_ReviewsId",
                        column: x => x.ReviewsId,
                        principalTable: "ReviewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GenresMovie",
                columns: table => new
                {
                    Genresid = table.Column<string>(type: "TEXT", nullable: false),
                    MoviesId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenresMovie", x => new { x.Genresid, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenresMovie_Genres_Genresid",
                        column: x => x.Genresid,
                        principalTable: "Genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenresMovie_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenresMovie_MoviesId",
                table: "GenresMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_ReviewsId",
                table: "Movie",
                column: "ReviewsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewModel_Authorid",
                table: "ReviewModel",
                column: "Authorid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteMovies");

            migrationBuilder.DropTable(
                name: "GenresMovie");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "ReviewModel");

            migrationBuilder.DropTable(
                name: "UserShortModel");
        }
    }
}
