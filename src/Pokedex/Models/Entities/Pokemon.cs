﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models.Entities
{
    public class Pokemon
    {
        public Pokemon()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Range(1, 801, ErrorMessage = "Pokedex number does not exist")]
        [Display(Name = "Pokedex Number")]
        public int PokedexNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Base Hitpoints")]
        public double BaseHitpoints { get; set; }

        [Required]
        [Display(Name = "Base Attack")]
        public double BaseAttack { get; set; }

        [Required]
        [Display(Name = "Base Defense")]
        public double BaseDefense { get; set; }

        [Required]
        [Display(Name = "Base Speed")]
        public double BaseSpeed { get; set; }

        [Required]
        [Display(Name = "Base Special Attack")]
        public double BaseSpecialAttack { get; set; }

        [Required]
        [Display(Name = "Base Special Defense")]
        public double BaseSpecialDefense { get; set; }

        //[Required]
        //public List<string> ImagePath { get; set; }

        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Description must be between {1} and {0} characters long.")]
        public string Description { get; set; }

        [Required]
        public bool IsInMod { get; set; }

        public TamingType tamingType { get; set; }

        public List<PokemonAttack> PokemonAttacks { get; set; }

        [NotMapped]
        public string DateReleased { get; set; }     

        public double Height { get; set; }

        public long TerminalCost { get; set; }

        public string AdminSpawnCheat { get; set; }

        public enum TamingType
        {
            PASSIVE = 1,
            KO_MEAT = 2,
            KO_BERRIES = 3,
            TERMINAL_PURCHASE = 4

        }
    }
}