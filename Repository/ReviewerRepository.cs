using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.
                Where(r => r.Id == id).
                Include(e => e.Reviews).
                FirstOrDefault();
        }
            

        public ICollection<Reviewer> GetReviewers() =>
            _context.Reviewers.ToList();


        public ICollection<Review> GetReviewsByReviewer(int id) =>
            _context.Reviews.Where(r => r.Reviewer.Id == id).ToList();
        

        public bool ReviewerExists(int id) =>
            _context.Reviewers.Any(r => r.Id == id);


        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Reviewers.Add(reviewer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
