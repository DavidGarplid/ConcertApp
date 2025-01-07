using ConcertApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using ConcertApp.Data.Entity;
namespace ConcertApp.Data;
public class ApplicationDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Concert> Concerts { get; set; }
    public virtual DbSet<Performance> Performances { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    // EF Core Fluent API: https://www.learnentityframeworkcore.com/configuration/fluent-api
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>         
        { 
            entity.ToTable("Users");
            entity.HasKey(u => u.ID); //Key
            entity.Property(u => u.email).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Concert>(entity => 
        {
            entity.ToTable("Concerts");
            entity.HasKey(c => c.Id); //Key
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Description).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Performance>(entity => 
        {
            entity.ToTable("Performances");
            entity.HasKey(p => p.ID);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.DateTime).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Location).IsRequired().HasMaxLength(100);
            entity.HasOne(p => p.Concert)
            .WithMany(c => c.Performances)
            .HasForeignKey(p => p.ConcertId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Booking>(entity => 
        {
            entity.ToTable("Bookings");
            entity.HasKey(b  => b.Id);
            entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
            entity.Property(b => b.Email).IsRequired().HasMaxLength(100);

            entity.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            //entity.HasOne(b => b.Performance)
            //.WithMany(p => p.Bookings)
            //.HasForeignKey(p => p.Id)
            //.OnDelete(DeleteBehavior.Restrict);

            SeedData(modelBuilder);
        });
    }
    private void SeedData(ModelBuilder builder)
    {

    }
} 
