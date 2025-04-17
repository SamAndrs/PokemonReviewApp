
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }


        // GET: api/ Review
        [HttpGet]
        public IActionResult GetReviews()
        {
            List<ReviewDto> reviews = new List<ReviewDto>();

            foreach(var review in _reviewRepository.GetReviews())
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
        }

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
        }

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
        }
    }
}
