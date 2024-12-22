using ConcertApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Todo.Data.Entity;
namespace Todo.Data;
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

    }
    private void SeedData(ModelBuilder builder)
    {

    }
} 
