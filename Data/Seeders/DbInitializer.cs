using AdoptAPet.Models;
using Microsoft.EntityFrameworkCore;

namespace AdoptAPet.Data.Seeders;

public class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new DataContext(
            serviceProvider.GetRequiredService<DbContextOptions<DataContext>>());
        
        // Seed Roles
        if (!context.Roles.Any())
        {
            var roles = new Role[]
            {
                new Role { Name = "Admin" },
                new Role { Name = "User" }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        // Seed Breeds
        if (!context.Breeds.Any())
        {
            var breeds = new Breed[]
            {
                new Breed { Name = "Labrador" },
                new Breed { Name = "Beagle" },
                new Breed { Name = "Bulldog" },
                new Breed { Name = "Poodle" },
                new Breed { Name = "German Shepherd" }
            };

            context.Breeds.AddRange(breeds);
            context.SaveChanges();
        }

        // Seed Shelters
        if (!context.Shelters.Any())
        {
            var shelters = new Shelter[]
            {
                new Shelter { Name = "Happy Tails", Address = "123 Main St", PhoneNumber = "123-456-7890" },
                new Shelter { Name = "Safe Haven", Address = "456 Elm St", PhoneNumber = "987-654-3210" },
                new Shelter { Name = "Cozy Paws", Address = "789 Oak St", PhoneNumber = "555-123-4567" },
                new Shelter { Name = "Furry Friends", Address = "321 Pine St", PhoneNumber = "555-987-6543" },
                new Shelter { Name = "Pet Paradise", Address = "654 Maple St", PhoneNumber = "555-654-3210" }
            };

            context.Shelters.AddRange(shelters);
            context.SaveChanges();
        }

        // Retrieve the seeded shelters
        var seededShelters = context.Shelters.ToList();
        var seededBreeds = context.Breeds.ToList();

        // Seed Pets
        if (context.Pets.Any()) return;
        var pets = new Pet[]
        {
            new Pet { Name = "Buddy", Age = 3, Description = "Friendly dog", IsAdopted = false, ShelterId = seededShelters[0].Id, BreedId = seededBreeds[0].Id },
            new Pet { Name = "Max", Age = 2, Description = "Playful puppy", IsAdopted = false, ShelterId = seededShelters[1].Id, BreedId = seededBreeds[1].Id },
            new Pet { Name = "Bella", Age = 4, Description = "Loyal companion", IsAdopted = false, ShelterId = seededShelters[2].Id, BreedId = seededBreeds[2].Id },
            new Pet { Name = "Charlie", Age = 1, Description = "Energetic and fun", IsAdopted = false, ShelterId = seededShelters[3].Id, BreedId = seededBreeds[3].Id },
            new Pet { Name = "Lucy", Age = 5, Description = "Gentle and loving", IsAdopted = false, ShelterId = seededShelters[4].Id, BreedId = seededBreeds[4].Id }
        };

        context.Pets.AddRange(pets);
        context.SaveChanges();

        var seededUsers = context.Users.ToList();
        var seededPets = context.Pets.ToList();
        // Seed AdoptionRequests
        if (!context.AdoptionRequests.Any())
        {
            var adoptionRequests = new AdoptionRequest[]
            {
                new AdoptionRequest { Date = DateTime.Now, IsApproved = false, UserId = seededUsers[0].Id, PetId = seededPets[0].Id },
                new AdoptionRequest { Date = DateTime.Now, IsApproved = false, UserId = seededUsers[2].Id, PetId = seededPets[1].Id },
                new AdoptionRequest { Date = DateTime.Now, IsApproved = false, UserId = seededUsers[0].Id, PetId = seededPets[2].Id },
                new AdoptionRequest { Date = DateTime.Now, IsApproved = false, UserId = seededUsers[2].Id, PetId = seededPets[3].Id },
                new AdoptionRequest { Date = DateTime.Now, IsApproved = false, UserId = seededUsers[0].Id, PetId = seededPets[4].Id }
            };

            context.AdoptionRequests.AddRange(adoptionRequests);
            context.SaveChanges();
        }
    }
}