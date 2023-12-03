using Microsoft.EntityFrameworkCore;
using Users.Microservice.Models;

namespace Users.Microservice.Infrastructure;

public class Repository : DbContext
{
    #region Constructors

    public Repository(DbContextOptions options) : base(options)
    {
    }

    #endregion

    #region Entities

    public required DbSet<User> Users { get; set; }

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