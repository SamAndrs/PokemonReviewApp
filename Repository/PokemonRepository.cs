﻿using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }

        public Pokemon GetPokemon(int id) => 
            _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();


        public Pokemon GetPokemon(string name) => 
            _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        

        public decimal GetPokemonRating(int id)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == id);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }


        public ICollection<Pokemon> GetPokemons() => 
            _context.Pokemons.OrderBy(p => p.Id).ToList();


        public bool PokemonExists(int id) => 
            _context.Pokemons.Any(p => p.Id == id);

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
