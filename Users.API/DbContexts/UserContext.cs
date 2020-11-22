using System;
using Microsoft.EntityFrameworkCore;
using Users.API.Entities;

namespace Users.API.DbContexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
        }

        // Another DbSet can be added and seeded identically to below.
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Database seeding with fictitious data
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Jason",
                    LastName = "Morsley",
                    DateOfBirth = new DateTime(1998, 12, 5),
                    Location = "Luton"
                },
                new User()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1982, 5, 21),
                    Location = "London"
                },
                new User()
                {
                    Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    DateOfBirth = new DateTime(2000, 12, 16),
                    Location = "Northampton"
                },
                new User()
                {
                    Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1982, 3, 6),
                    Location = "Hull"
                },
                new User()
                {
                    Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                    FirstName = "Tom",
                    LastName = "Reyson",
                    DateOfBirth = new DateTime(1969, 11, 23),
                    Location = "Northampton"
                },
                new User()
                {
                    Id = Guid.Parse("2aadd2df-7caf-45ab-9355-7f6332985a87"),
                    FirstName = "Dick",
                    LastName = "Ridley",
                    DateOfBirth = new DateTime(1970, 4, 5),
                    Location = "Luton"
                },
                new User()
                {
                    Id = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"),
                    FirstName = "Harry",
                    LastName = "McAlister",
                    DateOfBirth = new DateTime(1990, 10, 11),
                    Location = "London"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
