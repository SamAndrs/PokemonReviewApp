using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public Owner GetOwner(int id) =>
            _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        
        
        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId) =>
            _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeId).Select(o => o.Owner).ToList();
        

        public ICollection<Owner> GetOwners() =>
            _context.Owners.OrderBy(o => o.Id).ToList();


        public ICollection<Pokemon> GetPokemonByOwner(int ownerId) =>
        _context.PokemonOwners.Where(po => po.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        

        public bool OwnerExists(int ownerId) => 
            _context.Owners.Any(o => o.Id == ownerId);

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
