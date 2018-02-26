using System;
using System.Linq;
using DanielStoreFront.Models;

namespace DanielStoreFront
{
    internal class DBInitializer
    {
        internal static void Initialize(DanielTestContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new Categories {
                    //Id = 1,
                    Name = "Bombs",
                    DateCreated = DateTime.Now,
                },
                 new Categories{
                    //Id = 2,
                    Name = "Missiles",
                    DateCreated = DateTime.Now,
                },
                 new Categories{
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
                    Price = 2000000m,
                    ExplosiveYield = 16,
                    Description = "The Original.",
                    DateCreated = DateTime.Now,

                },
                new Products{
                    //Id = 2,
                    Name = "Fat Man",
                    Price = 2000000m,
                    ExplosiveYield = 18,
                    Description = "Little Boy's bigger brother.",
                    DateCreated = DateTime.Now,

                },
                new Products
                {
                    //Id = 3,
                    Name = "Tzar Bomba",
                    Price = 2000000000m,
                    ExplosiveYield = 100000,
                    Description = "The king of all bombs, so powerful that the russians were scared to test it at full power!",
                    DateCreated = DateTime.Now,
                },
                new Products
                {
                    //Id = 4, 
                    Name = "Yellow Cake",
                    Price = 20000m,
                    ExplosiveYield = 0,
                    Description = "Good ol' yellow cake sold by the kilogram",
                    DateCreated = DateTime.Now,
                }
                );
            }
            context.SaveChanges();
        }
    }
}