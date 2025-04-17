using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: All
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = new List<CategoryDto>();

            foreach (var category in _categoryRepository.GetAllCategories())
            {
                CategoryDto newDto = new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name
                };

                categories.Add(newDto);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }// End

        // GET: id
        [HttpGet("{categoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExist(categoryId))
                return NotFound();

            var category = _categoryRepository.GetCategory(categoryId);

            CategoryDto categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoryDto);
        }// End

        // GET: byCategory/ id
        [HttpGet("pokemon/{categoryId}")]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            List<PokemonDto> pokemons = new List<PokemonDto>();

            foreach(var pokemon in _categoryRepository.GetPokemonByCategory(categoryId))
            {
                var pokemonDto = new PokemonDto()
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


        // POST: api/ Category 
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetAllCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Category newCategory = new Category()
            {
                Id = categoryCreate.Id,
                Name = categoryCreate.Name
            };

            if(!_categoryRepository.CreateCategory(newCategory))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }// End
    
    }
}
