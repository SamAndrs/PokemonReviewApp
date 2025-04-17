
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewerController(IReviewerRepository reviewerRepository)
        {
            _reviewerRepository = reviewerRepository;
        }


        // GET: api/ Reviewer
        [HttpGet]
        public IActionResult GetReviews()
        {
            List<ReviewerDto> reviewers = new List<ReviewerDto>();

            foreach(var reviewer in _reviewerRepository.GetReviewers())
            {
                ReviewerDto newDto = new ReviewerDto()
                {
                    Id = reviewer.Id,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName,
                    Reviews = reviewer.Reviews
                };

                reviewers.Add(newDto);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }// End

        // GET: api/ Reviewer/ id
        [HttpGet("{reviewerId}")]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            var reviewer = _reviewerRepository.GetReviewer(reviewerId);

            ReviewerDto newDto = new ReviewerDto()
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName,
                Reviews = reviewer.Reviews
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(newDto);
        }// End

        // GET: api/ id/ reviews
        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            List<ReviewDto> reviews = new List<ReviewDto>();

            foreach(var review in _reviewerRepository.GetReviewsByReviewer(reviewerId))
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
