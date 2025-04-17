using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if(!_context.PokemonOwners.Any())
            {
                var pokemonOwners = new List<PokemonOwner>()
                {
                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Pikachu",
                            BirthDate = new DateTime(1903,1,1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory {Category = new Category() {Name = "Electric"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title = "Pikachu", Text = "Pikachu is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer() { FirstName = "Teddy", LastName = "Smith"} },

                                new Review { Title = "Pikachu", Text = "Pikachu is the best at killing rocks", Rating = 5,
                                Reviewer = new Reviewer() { FirstName = "Taylor", LastName = "Jones" } },

                                new Review { Title = "Pikachu", Text = "Pikachu, pikachu, pikachu", Rating = 1,
                                Reviewer = new Reviewer() { FirstName = "Jessica", LastName = "McGregor"} },
                            }

                        },

                        Owner = new Owner()
                        {
                            FirstName = "Jack",
                            LastName = "London",
                            Gym = "Brocks Gym",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },

                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Squirtle",
                            BirthDate = new DateTime(1903,1,1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title ="Squirtle", Text="Squirtle is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer() { FirstName = "Teddy", LastName = "Smith"} },
                                
                                new Review { Title ="Squirtle", Text="Squirtle is the best at killing rocks", Rating = 5,
                                Reviewer = new Reviewer() { FirstName = "Taylor", LastName = "Smith"} },

                                new Review { Title ="Squirtle", Text="Squirtle, squirtle, squirtle", Rating = 1,
                                Reviewer = new Reviewer() { FirstName = "Jessica", LastName = "McGregor"} },
                            }
                        },

                        Owner = new Owner()
                        {
                            FirstName = "Harry",
                            LastName = "Potter",
                            Gym = "Mistys Gym",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },

                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Venausuar",
                            BirthDate = new DateTime(1903,1,1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category() {Name = "leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title = "Venausuar", Text = "Venausuar is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer() { FirstName = "Teddy", LastName = "Smith"} },

                                new Review { Title = "Venausuar", Text = "Venausuar is best at killing rocks", Rating = 5,
                                Reviewer = new Reviewer() { FirstName = "Taylor", LastName = "Jones"} },

                                new Review { Title = "Venausuar", Text = "Venausuar, Venausuar, Venausuar", Rating = 1 ,
                                Reviewer = new Reviewer() { FirstName = "Jessica", LastName = "McGregor"} },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Ash",
                            LastName = "Ketchum",
                            Gym = "Ash's Gym",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };

                _context.PokemonOwners.AddRange(pokemonOwners);
                _context.SaveChanges();
            }
        }
    }


}
