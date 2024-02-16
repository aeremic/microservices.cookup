using Microsoft.EntityFrameworkCore;
using Recipes.Microservice.Domain;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Infrastructure;

public class ApplicationDbContext : DbContext
{
    #region Constructors

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    #endregion

    #region Entities

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }
    
    public DbSet<User> Users { get; set; }

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