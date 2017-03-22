using System;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models.Entities
{
    public class HomeContent
    {
        public HomeContent()
        {

        }

        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Body { get; set; }

        public string AuthoredBy { get; set; }

        public DateTime DatePosted { get; set; }
        
    }
}