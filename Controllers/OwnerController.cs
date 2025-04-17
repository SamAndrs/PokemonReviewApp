using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;

        public readonly ICountryRepository _countryRepository;

        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
        }

        // GET: api/Owner/
        [HttpGet]
        public IActionResult GetOwners()
        {
            List<OwnerDto> owners = new List<OwnerDto>();

            foreach(var owner in _ownerRepository.GetOwners())
            {
                OwnerDto getOwner = new OwnerDto()
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Gym = owner.Gym
                };

                owners.Add(getOwner);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }// End

        // GET: api/owner/ id
        [HttpGet("{ownerId}")]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _ownerRepository.GetOwner(ownerId);

            OwnerDto getOwner = new OwnerDto()
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Gym = owner.Gym
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(getOwner);
        }// End

        // GET: api/owner/ pokemonId
        [HttpGet("/pokemon/{pokeId}")]
        public IActionResult GetOwnersOfPokemon(int pokeId)
        {
            List<OwnerDto> owners = new List<OwnerDto>();

            foreach(var owner in _ownerRepository.GetOwnerOfAPokemon(pokeId))
            {
                OwnerDto ownerDto = new OwnerDto()
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Gym = owner.Gym
                };

                owners.Add(ownerDto);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }// End
        
        // GET: ownerId/pokemon
        [HttpGet("{ownerId}/pokemon")]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            List<PokemonDto> pokemons = new List<PokemonDto>();
            
            foreach(var pokemon in _ownerRepository.GetPokemonByOwner(ownerId))
            {
                PokemonDto pokemonDto = new PokemonDto()
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    BirthDate = pokemon.BirthDate
                };

                pokemons.Add(pokemonDto);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }// End

        // POST: api/owner/ owner
        [HttpPost]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto createOwner)
        {
            if (createOwner == null)
                return BadRequest(ModelState);

            var owner = _ownerRepository.GetOwners()
                .Where(o => o.LastName.Trim().ToUpper() == createOwner.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(owner != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = _countryRepository.GetCountry(countryId);

            Owner newOwner = new Owner()
            {
                Id = createOwner.Id,
                FirstName = createOwner.FirstName,
                LastName = createOwner.LastName,
                Gym = createOwner.Gym,
                Country = country
            };


            if(!_ownerRepository.CreateOwner(newOwner))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }// End
    }
}
