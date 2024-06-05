using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sofa.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "music_artists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_music_artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "music_paths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoScan = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastScan = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_music_paths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "music_albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CoverPath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ReleaseYear = table.Column<int>(type: "INTEGER", nullable: false),
                    TrackCount = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ArtistId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_music_albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_music_albums_music_artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "music_artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsPublic = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_playlists_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "musics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    AlbumId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArtistId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Genres = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    BitRate = table.Column<int>(type: "INTEGER", nullable: false),
                    TrackNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_musics_music_albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "music_albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_musics_music_artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "music_artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "music_ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicTrackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Rate = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_music_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_music_ratings_musics_MusicTrackId",
                        column: x => x.MusicTrackId,
                        principalTable: "musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_music_ratings_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "played_musics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicTrackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_played_musics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_played_musics_musics_MusicTrackId",
                        column: x => x.MusicTrackId,
                        principalTable: "musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_played_musics_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playlist_details",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicTrackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlist_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_playlist_details_musics_MusicTrackId",
                        column: x => x.MusicTrackId,
                        principalTable: "musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_playlist_details_playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_music_albums_ArtistId",
                table: "music_albums",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_music_ratings_MusicTrackId",
                table: "music_ratings",
                column: "MusicTrackId");

            migrationBuilder.CreateIndex(
                name: "IX_music_ratings_UserId",
                table: "music_ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_musics_AlbumId",
                table: "musics",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_musics_ArtistId",
                table: "musics",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_musics_Genres",
                table: "musics",
                column: "Genres");

            migrationBuilder.CreateIndex(
                name: "IX_musics_Title",
                table: "musics",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_played_musics_MusicTrackId",
                table: "played_musics",
                column: "MusicTrackId");

            migrationBuilder.CreateIndex(
                name: "IX_played_musics_UserId",
                table: "played_musics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_playlist_details_MusicTrackId",
                table: "playlist_details",
                column: "MusicTrackId");

            migrationBuilder.CreateIndex(
                name: "IX_playlist_details_PlaylistId",
                table: "playlist_details",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_playlists_UserId",
                table: "playlists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "music_paths");

            migrationBuilder.DropTable(
                name: "music_ratings");

            migrationBuilder.DropTable(
                name: "played_musics");

            migrationBuilder.DropTable(
                name: "playlist_details");

            migrationBuilder.DropTable(
                name: "musics");

            migrationBuilder.DropTable(
                name: "playlists");

            migrationBuilder.DropTable(
                name: "music_albums");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "music_artists");
        }
    }
}
