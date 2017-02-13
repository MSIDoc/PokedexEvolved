using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pokedex.Models.Contexts;
using Pokedex.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public static class PokemonHelper
    {
              
        public static async Task<bool> ProcessImages(ICollection<IFormFile> files, Pokemon pokemon, PokedexContext _context, IHostingEnvironment _environment, ModelStateDictionary m)
        {


            if (m.IsValid)
            {

                if (pokemon.ID == 0)
                    _context.Pokemon.Add(pokemon);
                else
                    _context.Pokemon.Update(pokemon);
                    
                
                await _context.SaveChangesAsync();

                var uploadDir = Path.Combine(_environment.WebRootPath, $"images\\pokemon\\{pokemon.Name.ToLower()}");
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileGuid = Guid.NewGuid();

                        if (!Directory.Exists(uploadDir))
                            Directory.CreateDirectory(uploadDir);
                        var fsName = $"{fileGuid.ToString()}.{Path.GetExtension(file.FileName)}";

                        var filePath = Path.Combine(uploadDir, fsName);

                        var pokemonImage = new PokemonImage()
                        {
                            Pokemon = pokemon,
                            Active = true,
                            Caption = "",
                            ImageName = file.FileName,
                            FileSystemName = fsName,
                            PokemonID = pokemon.ID
                        };

                        using (var fs = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fs);

                        }

                        _context.PokemonImages.Add(pokemonImage);
                    }
                }
                if (_context.ChangeTracker.HasChanges())
                    await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }



        public static string FormatArkValue(decimal val)
        {
            return val.ToString("00.00");
        }

    }
}
