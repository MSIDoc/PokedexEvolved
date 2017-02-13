using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models.Entities
{
    public class PokemonAttack
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(75)")]
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
