using ConcertApp.Data.Entity;
using Microsoft.EntityFrameworkCore;

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
            entity.HasKey(c => c.ID); //Key
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Description).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Performance>(entity => 
        {
            entity.ToTable("Performances");
            entity.HasKey(p => p.ID);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.DateTime).IsRequired();
            entity.Property(p => p.Location).IsRequired().HasMaxLength(100);

            entity.HasOne(p => p.Concert)
            .WithMany(c => c.Performances)
            .HasForeignKey(p => p.ConcertId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Booking>(entity => 
        {
            entity.ToTable("Bookings");
            entity.HasKey(b  => b.ID);
            entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
            entity.Property(b => b.Email).IsRequired().HasMaxLength(100);

            entity.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Performance)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PerformanceID)
            .OnDelete(DeleteBehavior.Restrict);
        });
        SeedData(modelBuilder);
    }
    private static void SeedData(ModelBuilder modelBuilder)
    {
        // ✅ Seed Users
        modelBuilder.Entity<User>().HasData(
            new User { ID = 5, name = "John Doe", email = "johndoe@example.com", password = "Password123" },
            new User { ID = 6, name = "Jane Smith", email = "janesmith@example.com", password = "Str0ngP@ssword!" }
        );

        // ✅ Seed Concerts
        modelBuilder.Entity<Concert>().HasData(
            new Concert { ID = 1, Name = "Rock Concert", Description = "A night of amazing rock music." },
            new Concert { ID = 2, Name = "Jazz Night", Description = "Smooth jazz performances all evening." }
        );

        // ✅ Seed Performances
        modelBuilder.Entity<Performance>().HasData(
        new Performance { ID = 1, Name = "Opening Act", Location = "Main Stage", DateTime = new DateTime(2025, 02, 20, 18, 00, 00), ConcertId = 1 },
        new Performance { ID = 2, Name = "Metallica", Location = "Main Stage", DateTime = new DateTime(2025, 02, 20, 21, 00, 00), ConcertId = 1 },
        new Performance { ID = 3, Name = "Jazz Ensemble", Location = "Jazz Club", DateTime = new DateTime(2025, 02, 21, 19, 30, 00), ConcertId = 2 },
        new Performance { ID = 4, Name = "Dj Huvudvärk", Location = "DJ Stage", DateTime = new DateTime(2025, 02, 21, 22, 00, 00), ConcertId = 2 }
        );

        // ✅ Seed Bookings (Ensure IDs match Performance IDs)
        modelBuilder.Entity<Booking>().HasData(
            new Booking { ID = 1, Name = "John's Rock Booking", Email = "johndoe@example.com", UserId = 5, PerformanceID = 2 },
            new Booking { ID = 2, Name = "Jane's Jazz Booking", Email = "janesmith@example.com", UserId = 6, PerformanceID = 3 }
        );
    }
} 
