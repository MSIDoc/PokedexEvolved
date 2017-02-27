namespace Pokedex.Models.Entities
{
    public class PokemonHarvestable
    {
        public Pokemon pokemon { get; set; }

        public Harvestables harvestable { get; set; }
    }
}