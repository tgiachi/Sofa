using Microsoft.EntityFrameworkCore;
using Sofa.Database.Entities;

namespace Sofa.Database.Context;

public class SofaDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<MusicPathEntity> MusicPaths { get; set; }

    public DbSet<MusicTrackEntity> MusicTracks { get; set; }

    public DbSet<MusicArtistEntity> Artists { get; set; }

    public DbSet<MusicRatingEntity> MusicRatings { get; set; }

    public DbSet<PlayedMusicEntity> PlayedMusics { get; set; }

    public DbSet<PlaylistEntity> Playlists { get; set; }

    public DbSet<PlaylistDetailEntity> PlaylistDetails { get; set; }

    public SofaDbContext(DbContextOptions<SofaDbContext> options) : base(options)
    {
    }

    public SofaDbContext()
    {
    }
    //
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("Data Source=sofa.db");
    // }
}
