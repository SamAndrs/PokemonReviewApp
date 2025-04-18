
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository, 
            IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
           _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }


        // GET: api/ Review
        [HttpGet]
        public IActionResult GetReviews()
        {
            List<ReviewDto> reviews = new List<ReviewDto>();

            foreach (var review in _reviewRepository.GetReviews())
            {
                ReviewDto newDto = new ReviewDto()
                {
                    Id = review.Id,
                    Title = review.Title,
                    Text = review.Text,
                    Rating = review.Rating
                };

                reviews.Add(newDto);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }// End

        // GET: api/ Review/ id
        [HttpGet("{reviewId}")]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _reviewRepository.GetReview(reviewId);

            ReviewDto newDto = new ReviewDto()
            {
                Id = review.Id,
                Title = review.Title,
                Text = review.Text,
                Rating = review.Rating
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(newDto);
        }// End

        // GET: pokemon/ {Id}
        [HttpGet("pokemon/{pokeId}")]
        public IActionResult GetReviewsOfAPokemon(int pokeId)
        {
            List<ReviewDto> reviews = new List<ReviewDto>();

            foreach (var review in _reviewRepository.GetReviewsOfAPokemon(pokeId))
            {
                ReviewDto newDto = new ReviewDto()
                {
                    Id = review.Id,
                    Title = review.Title,
                    Text = review.Text,
                    Rating = review.Rating
                };

                reviews.Add(newDto);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }// End

        // POST
        [HttpPost]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId,[FromBody] ReviewDto reviewCreate )
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Review newReview = new Review()
            {
                Id = reviewCreate.Id,
                Title = reviewCreate.Title,
                Text = reviewCreate.Text,
                Rating = reviewCreate.Rating,

                Reviewer = _reviewerRepository.GetReviewer(reviewerId),
                Pokemon = _pokemonRepository.GetPokemon(pokeId)
            };


            if(!_reviewRepository.CreateReview(newReview))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully saved!");
        }// End
    }
}
