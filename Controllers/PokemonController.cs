﻿using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        // GET: All
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            //var pokemons = _pokemonRepository.GetPokemons();

            var pokemons = new List<PokemonDto>();
            
            //foreach(var pokemon in pokemons)
            foreach(var pokemon in _pokemonRepository.GetPokemons())
            {
                var pokeDto = new PokemonDto()
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    BirthDate = pokemon.BirthDate
                };
                pokemons.Add(pokeDto);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }// End

        // GET: id
        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var pokemon = _pokemonRepository.GetPokemon(pokeId);

            var pokemonDto = new PokemonDto()
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                BirthDate = pokemon.BirthDate
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //return Ok(pokemon);
            return Ok(pokemonDto);
        }// End

        // GET:id/ rating
        [HttpGet("{pokeId}/rating")]
        //[ProducesResponseType(200, Type = typeof(Pokemon))]
        //[ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var rating = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }// End

        // POST: TO DO
    }
}
