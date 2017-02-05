using Microsoft.Extensions.Configuration;
using Pokedex.Models.Entities;
using System.Collections.Generic;

namespace Pokedex.Models.ViewModels
{
    public class PokeView
    {
        private readonly IConfigurationRoot _config;

        public PokeView(IConfigurationRoot config)
        {
            _config = config;
        }

        public Pokemon Pokemon { get; set; }
        
        public ArkPokemonStats PokemonArkStats { get { return new ArkPokemonStats(Pokemon, _config); } }

        public List<Pokemon> PokemonList { get; set; }

        public List<PokemonImage> PokeImages { get; set; }

    }
}
