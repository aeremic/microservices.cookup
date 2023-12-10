using Microsoft.EntityFrameworkCore;
using Recipes.Microservice.Domains;

namespace Recipes.Microservice.Infrastructure;

public class Repository : DbContext
{
    #region Constructors

    public Repository(DbContextOptions options) : base(options)
    {
    }

    #endregion

    #region Entities

    public required DbSet<Recipe> Recipes { get; set; }
    
    public required DbSet<Ingredient> Ingredients { get; set; }
    
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