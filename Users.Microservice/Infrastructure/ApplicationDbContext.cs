using Microsoft.EntityFrameworkCore;
using Users.Microservice.Domains;
using Users.Microservice.Domains.Models;

namespace Users.Microservice.Infrastructure;

public class ApplicationDbContext : DbContext
{
    #region Constructors

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    #endregion

    #region Entities

    public required DbSet<User> Users { get; init; }

    #endregion

    #region Configuration

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuration setup
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Foreign keys setup
    }

    #endregion
}