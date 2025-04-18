﻿using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExist(int id) => 
            _context.Categories.Any(c => c.Id == id);

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public ICollection<Category> GetAllCategories() => 
            _context.Categories.ToList();


        public Category GetCategory(int id) => 
            _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories
                .Where(c => c.CategoryId == categoryId)
                .Select(p => p.Pokemon)
                .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
