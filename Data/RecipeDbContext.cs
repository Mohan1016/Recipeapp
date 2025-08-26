using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RecepieApp.Models;

namespace RecipeApp.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
