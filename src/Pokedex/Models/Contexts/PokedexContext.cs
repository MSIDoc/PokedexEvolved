using Pokedex.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models.Contexts
{
    public class PokedexContext : DbContext
    {
        private readonly string _connectionString;

        public PokedexContext(DbContextOptions options, IConfigurationRoot builder) : base (options)
        {
            _connectionString = builder.GetConnectionString("Pokedex");
        }

        public DbSet<Pokemon> Pokemon { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(_connectionString);
    }
}
