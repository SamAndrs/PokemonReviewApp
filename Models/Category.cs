﻿namespace PokemonReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<PokemonCategory> PokemonCategories { get; set; }
    }
}
