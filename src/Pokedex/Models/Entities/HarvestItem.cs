using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models.Entities
{
    public class HarvestItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //[ForeignKey("HarvestableID")]
        //public Harvestables Harvestable { get; set; }
        
        public string Name { get; set; }        

        public bool IsHarvestable { get; set; }
    }
}
