
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

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
                    //Reviews = _reviewerRepository.GetReviewsByReviewer(reviewer.Id)
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
        }// End

        // POST
        [HttpPost]
        public IActionResult CreateReviewer([FromBody] ReviewerDto createReviewer)
        {
            if (createReviewer == null)
                return BadRequest(ModelState);

            var reviewer = _reviewerRepository.GetReviewers()
                .Where(r => r.LastName.Trim().ToUpper() == createReviewer.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            var newReviewer = new Reviewer()
            {
                Id = createReviewer.Id,
                FirstName = createReviewer.FirstName,
                LastName = createReviewer.LastName,
                //Reviews = new List<Review>()
            };


            if(reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully saved!");
        }
    }
}
