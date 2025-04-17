
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        // GET: api/ country/ All
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            List<CountryDto> countries = new List<CountryDto>();

            foreach(var country in _countryRepository.GetCountries())
            {
                CountryDto newCountry = new CountryDto()
                {
                    Id = country.Id,
                    Name = country.Name
                };

                countries.Add(newCountry);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }// End

        // GET: api/ country/ id
        [HttpGet("{countryId}")]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var country = _countryRepository.GetCountry(countryId);

            CountryDto newCountry = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(newCountry);
        }// End

        
        // GET: country/owner/ id
        [HttpGet("/country/owner/{ownerId}")]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country = _countryRepository.GetCountryByOwner(ownerId);

            CountryDto countryDto = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countryDto);
        }// End

        
        // GET: country/owners/ id
        [HttpGet("/country/owners/{countryId}")]
        public IActionResult GetOwnersFromCountry(int countryId)
        {
            List<OwnerDto> owners = new List<OwnerDto>();

            foreach(var owner in _countryRepository.GetOwnersFromACountry(countryId))
            {
                OwnerDto ownerDto = new OwnerDto()
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Gym = owner.Gym,
                };

                owners.Add(ownerDto); 
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }//End


        // POST: country/ country
        [HttpGet]
        public IActionResult CreateCountry([FromBody] CountryDto createCountry)
        {
            if (createCountry == null)
                return BadRequest(ModelState);

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == createCountry.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Country newCountry = new Country()
            {
                Id = createCountry.Id,
                Name = createCountry.Name
            };

            if(!_countryRepository.CreateCountry(newCountry))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }// End

    }
}
