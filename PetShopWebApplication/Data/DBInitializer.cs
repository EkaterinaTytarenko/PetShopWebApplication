using PetShopWebApplication.Data;
using PetShopWebApplication.Models;
using System;
using System.Linq;

namespace PetShopWebApplication.Data
{
    public static class DBInitializer
    {
        public static void Initialize(AnimalsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Animals.Any())
            {
               return;  
            }

            var shops = new Shop[]
             {
            new Shop {ID=1,Name="Ваші тварини",Adress="вул.Сеченова,буд 6"},
            new Shop {ID=2,Name="Дикі друзі",Adress="вул.Володимирська,буд.60"}
             };
            foreach (Shop s in shops)
            {
                context.Shops.Add(s);
            }
            context.SaveChanges();

            var cages = new Cage[]
            {
            new Cage {ID=1,ShopID=1,Number=1,Square=20},
            new Cage {ID=2,ShopID=1,Number=2,Square=30},
            new Cage {ID=3,ShopID=2,Number=3,Square=20},
            new Cage {ID=4,ShopID=2,Number=4,Square=25},
            new Cage {ID=5,ShopID=2,Number=5,Square=40},
            };
            foreach (Cage c in cages)
            {
                context.Cages.Add(c);
            }
            context.SaveChanges();

            var colors = new Color[]
            {
            new Color {ID=1,Name="білий"},
            new Color {ID=2,Name="чорний"},
            new Color {ID=3,Name="зелений"},
            new Color {ID=4,Name="рудий"},
            new Color {ID=5,Name="двокольоровий"},
            new Color {ID=6,Name="без кольору"},
            new Color {ID=7,Name="трикольоровий"},
            new Color {ID=8,Name="коричневий"},
            new Color {ID=9,Name="сірий"},
            new Color {ID=10,Name="золотий"}
            };
            foreach (Color c in colors)
            {
                context.Colors.Add(c);
            }
            context.SaveChanges();

            var food = new Food[]
            {
            new Food {ID=1,Name="трава"},
            new Food {ID=2,Name="корм\"Ваше цуценя\""},
            new Food {ID=3,Name="корм\"Ваше кошеня\""},
            new Food {ID=4,Name="корм\"Ваша рибка\""},
            new Food {ID=5,Name="м'ясо"},
            new Food {ID=6,Name="капуста"},
            new Food {ID=7,Name="молоко"},
            new Food {ID=8,Name="корм\"Ваш гризун\""},
            new Food {ID=9,Name="зерно"},
            new Food {ID=10,Name="морква"}
            };
            foreach (Food f in food)
            {
                context.Food.Add(f);
            }
            context.SaveChanges();

            

            var species = new Species[]
            {new Species{ID=1,Name="кіт",LifeTime=18,Temperature=20},
             new Species{ID=2,Name="собака",LifeTime=19,Temperature=19},
             new Species{ID=3,Name="коза",LifeTime=16,Temperature=21},
             new Species{ID=4,Name="рибка",LifeTime=1,Temperature=25},
             new Species{ID=5,Name="хом'як",LifeTime=2,Temperature=22}
            };
            foreach (Species s in species)
            {
                context.Species.Add(s);
            }
            context.SaveChanges();

            var speciesfood = new SpeciesFood[]
            {new SpeciesFood{ID=1,SpeciesID=1,FoodID=5 },
            new SpeciesFood{ID=2,SpeciesID=1,FoodID=3 },
            new SpeciesFood{ID=3,SpeciesID=1,FoodID=7 },
            new SpeciesFood{ID=4,SpeciesID=2,FoodID=5 },
            new SpeciesFood{ID=5,SpeciesID=2,FoodID=2 },
            new SpeciesFood{ID=6,SpeciesID=3,FoodID=1 },
             new SpeciesFood{ID=7,SpeciesID=3,FoodID=6 },
              new SpeciesFood{ID=8,SpeciesID=3,FoodID=10 },
               new SpeciesFood{ID=9,SpeciesID=4,FoodID=4 },
               new SpeciesFood{ID=10,SpeciesID=5,FoodID=9},
               new SpeciesFood{ID=11,SpeciesID=5,FoodID=8 },
               new SpeciesFood{ID=12,SpeciesID=5,FoodID=10 }
            };
            foreach (SpeciesFood sf in speciesfood)
            {
                context.SpeciesFood.Add(sf);
            }
            context.SaveChanges();

            var animals = new Animal[]
           {
            new Animal {ID=1,Name="Мурка",Sex='Ж',ShopID=1,CageID=1,SpeciesID=1,Date=DateTime.Parse("2018-09-01"),ColorID=1,Price=100},
            new Animal {ID=2,Name="Мурчик",Sex='Ч',ShopID=2,CageID=4,SpeciesID=1,Date=DateTime.Parse("2018-12-10"),ColorID=5,Price=150},
            new Animal {ID=3,Name="Ксюша",Sex='Ж',ShopID=1,CageID=2,SpeciesID=3,Date=DateTime.Parse("2018-01-23"),ColorID=2,Price=10},
            new Animal {ID=4,Name="Шарік",Sex='Ч',ShopID=2,CageID=5,SpeciesID=2,Date=DateTime.Parse("2019-02-01"),ColorID=4,Price=250},
            new Animal {ID=5,Name="",Sex='Ж',ShopID=1,CageID=2,SpeciesID=4,Date=DateTime.Parse("2019-03-10"),ColorID=10,Price=50},
            new Animal {ID=6,Name="Хома",Sex='Ч',ShopID=2,CageID=3,SpeciesID=5,Date=DateTime.Parse("2018-12-21"),ColorID=4,Price=20}
           };
            foreach (Animal a in animals)
            {
                context.Animals.Add(a);
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            { }
        }
    }
}