using Pokedex.Models.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pokedex.Models.ViewModels
{
    public class CreatePokemonViewModel
    {

        public Pokemon pokemon { get; set; }

        public SelectList TamingTypes => new SelectList(new Dictionary<Pokemon.TamingType, string> {
            { Pokemon.TamingType.PASSIVE, "Passive" },
            { Pokemon.TamingType.KO_BERRIES, "KO Berry" },
            { Pokemon.TamingType.KO_MEAT, "KO Meat" },
            { Pokemon.TamingType.TERMINAL_PURCHASE, "Terminal Purchase" }
        },"Key", "Value");

        public List<PokemonImage> PokeImages { get; set; }

        public SelectList Harvestables { get; set; }
    }
}
