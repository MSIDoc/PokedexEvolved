using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models.Entities
{
    public class PokemonAttack
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public AttackButton attackButton { get; set; }

        public enum AttackButton
        {
            LEFT_CLICK = 1,
            RIGHT_CLICK = 2,
            C = 3
        }
    }
}
