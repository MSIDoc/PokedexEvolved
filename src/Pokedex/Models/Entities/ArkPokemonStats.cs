using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models.Entities
{
    public class ArkPokemonStats
    {
        private readonly Pokemon _pokemon;
        private readonly double _hitpoints;
        private readonly double _defenseHitpoints;
        private readonly double _damage;
        private readonly double _stamina;
        private readonly double _movementSpeed;
        private readonly double _torpor;
        private readonly double _weight;
        private readonly double _postEq;

        public ArkPokemonStats(Pokemon pokemon, IConfigurationRoot root)
        {
            if (pokemon != null)
                _pokemon = pokemon;

            if(root != null)
            {
                var StatModifiers = root.GetSection("PokemonStatModifiers");
                _hitpoints = double.Parse(StatModifiers["Hitpoints"]);
                _defenseHitpoints = double.Parse(StatModifiers["DefHp"]);
                _damage = double.Parse(StatModifiers["Damage"]);
                _stamina = double.Parse(StatModifiers["Stamina"]);
                _movementSpeed = double.Parse(StatModifiers["MovementSpeed"]);
                _torpor = double.Parse(StatModifiers["Torpor"]);
                _weight = double.Parse(StatModifiers["Weight"]);
                _postEq = double.Parse(StatModifiers["PostEq"]);
            }

        }
        
        public double ArkHitpoints => (
            _pokemon.BaseHitpoints * _hitpoints +
            _pokemon.BaseDefense * _defenseHitpoints)
            +
            (_pokemon.BaseHitpoints * _hitpoints +
            _pokemon.BaseDefense * _defenseHitpoints +
            _pokemon.BaseSpecialDefense * _defenseHitpoints) * _postEq;


        public double ArkStamina => (_pokemon.BaseSpeed * _stamina + _pokemon.BaseSpeed * _stamina * _postEq);

        public double ArkOxygen => 150;

        public double ArkDamage => _pokemon.BaseAttack * _damage + _pokemon.BaseAttack * _damage + _postEq;        
        
        public double ArkWeight => _weight * _pokemon.Height * ArkDamage;

        public double ArkMovementSpeed => _pokemon.BaseSpeed * _movementSpeed + _pokemon.BaseSpeed * _movementSpeed * _postEq;

        public double ArkTorpor => _pokemon.BaseDefense * _torpor;


    }
}
