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
            .HasForeignKey(p => p.PerformanceID)
            .OnDelete(DeleteBehavior.Restrict);

            SeedData(modelBuilder);
        });
    }
    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Users
        var user1 = new User
        {
            ID = 5,
            name = "John Doe",
            email = "johndoe@example.com",
            password = "P@ssw0rd123" // Ensure it meets the password requirements
        };

        var user2 = new User
        {
            ID = 6,
            name = "Jane Smith",
            email = "janesmith@example.com",
            password = "Str0ngP@ssword!"
        };

        modelBuilder.Entity<User>().HasData(user1, user2);

        // Seed Concerts
        var concert1 = new Concert
        {
            ID = 1,
            Name = "Rock Concert",
            Description = "A night of amazing rock music."
        };

        var concert2 = new Concert
        {
            ID = 2,
            Name = "Jazz Night",
            Description = "Smooth jazz performances all evening."
        };

        modelBuilder.Entity<Concert>().HasData(concert1, concert2);

        // Seed Performances
        var performance1 = new Performance
        {
            ID = 1,
            Name = "Opening Act",
            Location = "Main Stage",
            DateTime = DateTime.UtcNow.AddHours(1),
            ConcertId = concert1.ID // Links to Rock Concert
        };

        var performance2 = new Performance
        {
            ID = 2,
            Name = "Metallica",
            Location = "Main Stage",
            DateTime = DateTime.UtcNow.AddHours(3),
            ConcertId = concert1.ID // Links to Rock Concert
        };

        var performance3 = new Performance
        {
            ID = 3,
            Name = "Jazz Ensemble",
            Location = "Jazz Club",
            DateTime = DateTime.UtcNow.AddDays(1).AddHours(2),
            ConcertId = concert2.ID // Links to Jazz Night
        };

        var performance4 = new Performance
        {
            ID = 4,
            Name = "Dj Huvudvärk",
            Location = "DJ Stage",
            DateTime = DateTime.UtcNow.AddDays(1).AddHours(2),
            ConcertId = concert2.ID // Links to Jazz Night
        };

        modelBuilder.Entity<Performance>().HasData(performance1, performance2, performance3, performance4);

        // Seed Bookings
        var booking1 = new Booking
        {
            ID = 1,
            Name = "John's Rock Booking",
            Email = "johndoe@example.com",
            UserId = user1.ID, // Link to John Doe
            PerformanceID = performance2.ID // Will need to set a Performance ID if available
        };

        var booking2 = new Booking
        {
            ID = 2,
            Name = "Jane's Jazz Booking",
            Email = "janesmith@example.com",
            UserId = user2.ID, // Link to Jane Smith
            PerformanceID = performance3.ID // Will need to set a Performance ID if available
        };

        modelBuilder.Entity<Booking>().HasData(booking1, booking2);
    }
} 
