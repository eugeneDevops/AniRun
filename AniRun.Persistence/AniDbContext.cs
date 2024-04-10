using AniRun.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AniRun.Persistence;

public class AniDbContext : DbContext
{
    public DbSet<Media> Medias { get; set; }
    public DbSet<Studio> Studios { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    
    public AniDbContext(DbContextOptions<AniDbContext> options) : base(options){}
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("AniRun");
    }
}