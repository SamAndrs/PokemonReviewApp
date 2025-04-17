using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public Review GetReview(int id) =>
            _context.Reviews.Where(r => r.Id == id).FirstOrDefault();


        public ICollection<Review> GetReviews() =>
            _context.Reviews.ToList();


        public ICollection<Review> GetReviewsOfAPokemon(int pokeId) =>
            _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();



        public bool ReviewExists(int id) =>
            _context.Reviews.Any(r => r.Id == id);
        
    }
}
