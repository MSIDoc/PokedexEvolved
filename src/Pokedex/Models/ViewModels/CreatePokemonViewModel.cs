using Pokedex.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pokedex.Models.ViewModels
{
    public class CreatePokemonViewModel
    {

        public Pokemon pokemon { get; set; }

        public SelectList TamingTypes => new SelectList(new List<string> { "Passive", "KO Berry", "KO Meat", "Terminal Purchase" });
    }
}
