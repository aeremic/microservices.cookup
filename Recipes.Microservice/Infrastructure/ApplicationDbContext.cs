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
    
    public DbSet<UserRecipe> UserRecipes { get; set; }
    
    public DbSet<Comment> Comments { get; set; }

    #endregion

    #region Configuration

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRecipe>()
            .HasKey(ur => new { ur.UserId, ur.RecipeId });

        modelBuilder.Entity<UserRecipe>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRecipes)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRecipe>()
            .HasOne(ur => ur.Recipe)
            .WithMany(r => r.UserRecipes)
            .HasForeignKey(ur => ur.RecipeId);
        
        modelBuilder.Entity<Comment>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne(ur => ur.Recipe)
            .WithMany(r => r.Comments)
            .HasForeignKey(ur => ur.RecipeId);
    }

    #endregion
}