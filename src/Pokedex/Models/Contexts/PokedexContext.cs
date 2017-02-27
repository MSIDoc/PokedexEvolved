using Pokedex.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Pokedex.Models.Contexts
{
    public class PokedexContext : DbContext
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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(_connectionString);
    }
}
