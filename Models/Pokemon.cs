namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<PokemonOwner> PokemonOwners { get; set; }

        public virtual List<PokemonCategory> PokemonCategories { get; set; }
    }
}
