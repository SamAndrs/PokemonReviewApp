﻿namespace PokemonReviewApp.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Owner> Owners { get; set; }
    }
}
