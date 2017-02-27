using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models.Entities
{
    public class Harvestables
    {

        [Key]
        public int ID { get; set; }

        [DataType(DataType.Text)]   
        [Column(TypeName = "varchar(30)")]            
        public string Name { get; set; }

        

    }
}
