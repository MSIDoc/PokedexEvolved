using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models.Entities
{
    public class PokemonImage
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public string FileSystemName { get; set; }

        public int PokemonID { get; set; }

        public virtual Pokemon Pokemon { get; set; }

        public string Caption { get; set; }

        public bool Active { get; set; }

    }
}
