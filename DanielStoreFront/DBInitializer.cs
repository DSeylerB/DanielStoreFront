using System;
using System.Linq;
using DanielStoreFront.Models;
using Microsoft.EntityFrameworkCore;

namespace DanielStoreFront
{
    internal class DBInitializer
    {
        internal static void Initialize(DanielTestContext context)
        {
            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new Categories {
                    //Id = 1,
                    Name = "Bombs",
                    DateCreated = DateTime.Now,
                },
                 new Categories {
                     //Id = 2,
                     Name = "Missiles",
                     DateCreated = DateTime.Now,
                 },
                 new Categories {
                     //Id = 3,
                     Name = "Ordinance",
                     DateCreated = DateTime.Now,
                 },
                 new Categories
                 {
                     //Id = 4,
                     Name = "Materials",
                     DateCreated = DateTime.Now,
                 }
                );


            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(new Products
                {
                    //Id = 1,
                    Name = "Little Boy",
                    ImageUrl = "images/Little_boy.jpg",
                    Price = 2000000m,
                    ExplosiveYield = 16,
                    Description = "The Original.",
                    DateCreated = DateTime.Now,
                },
                new Products {
                    //Id = 2,
                    Name = "Fat Man",
                    ImageUrl = "images/Little_boy.jpg",
                    Price = 2000000m,
                    ExplosiveYield = 18,
                    Description = "Little Boy's bigger brother.",
                    DateCreated = DateTime.Now,

                },
                new Products
                {
                    //Id = 3,
                    Name = "Tzar Bomba",
                    ImageUrl = "images/Little_boy.jpg",
                    Price = 2000000000m,
                    ExplosiveYield = 100000,
                    Description = "The king of all bombs, so powerful that the russians were scared to test it at full power!",
                    DateCreated = DateTime.Now,
                },
                new Products
                {
                    //Id = 4, 
                    Name = "Yellow Cake",
                    ImageUrl = "images/Little_boy.jpg",
                    Price = 20000m,
                    ExplosiveYield = 0,
                    Description = "Good ol' yellow cake sold by the kilogram",
                    DateCreated = DateTime.Now,
                },
                 new Products
                 {
                     //Id = 5, 
                     Name = "SCUD",
                     ImageUrl = "images/Little_boy.jpg",
                     Price = 2000000m,
                     ExplosiveYield = 10000,
                     Description = "A big scary missile that harkens back to the cold war",
                     DateCreated = DateTime.Now,
                 },
                 new Products
                 {
                     //Id = 6, 
                     Name = "Casaba Howitzer",
                     ImageUrl = "images/Little_boy.jpg",
                     Price = 50000000m,
                     ExplosiveYield = 0,
                     Description = "What's more destructive than a conventional nuke? How about  bomb that focuses Its entire yield into a narrow cone! Now available via orbital strike.",
                     DateCreated = DateTime.Now,
                 }
                );
                context.SaveChanges();

                context.ProductsCategories.AddRange(new ProductsCategories
                {
                    CategoryId = context.Categories.First(x => x.Name == "Bombs").Id,
                    ProductId = context.Products.First(x => x.Name == "Little Boy").Id 
                },
                new ProductsCategories
                {
                    CategoryId = context.Categories.First(x => x.Name == "Bombs").Id,
                    ProductId = context.Products.First(x => x.Name == "Fat Man").Id
                },
                new ProductsCategories
                {
                    CategoryId = context.Categories.First(x => x.Name == "Bombs").Id,
                    ProductId = context.Products.First(x => x.Name == "Tzar Bomba").Id
                },
                new ProductsCategories
                {
                    CategoryId = context.Categories.First(x => x.Name == "Missiles").Id,
                    ProductId = context.Products.First(x => x.Name == "SCUD").Id
                },
                new ProductsCategories
                {
                    CategoryId = context.Categories.First(x => x.Name == "Ordinance").Id,
                    ProductId = context.Products.First(x => x.Name == "Casaba Howitzer").Id
                },
                new ProductsCategories
                {
                    CategoryId = context.Categories.First(x => x.Name == "Materials").Id,
                    ProductId = context.Products.First(x => x.Name == "Yellow Cake").Id
                });
                context.SaveChanges();
            }
            if (!context.Reviews.Any())
            {
                context.Reviews.Add(new Review
                {
                    Rating = 5,
                    Body = "This product is good!",
                    IsApproved = true,
                    Product = context.Products.First()
                });
                
            }
        }
        
        
    }
}