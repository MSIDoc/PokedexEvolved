using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models.Entities
{
    public class PokemonHabitat
    {

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

    }
}
