using Pokedex.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace Pokedex.Models.Contexts
{
    public class PokedexContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly string _connectionString;

        public PokedexContext(DbContextOptions options, IConfigurationRoot builder) : base (options)
        {
            _connectionString = builder.GetConnectionString("Pokedex");
        }
        
        public DbSet<Harvestables> Harvestables { get; set; }
        public DbSet<PokemonImage> PokemonImages { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<PokemonAttack> PokemonAttack { get; set; }
        public DbSet<PokemonHabitat> PokemonHabitat { get; set; }
        public DbSet<HomeContent> HomePageContent { get; set; }

   

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(_connectionString);
    }
}
