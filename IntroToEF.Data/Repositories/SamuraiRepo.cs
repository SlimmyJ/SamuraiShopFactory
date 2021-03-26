using IntroToEF.Data.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IntroToEF.Data.Repositories
{
    public class SamuraiRepo : ISamuraiRepo
    {
        private SamuraiContext context;

        public SamuraiRepo()
        {
            // Open connection to database
            context = new SamuraiContext();
        }

        public void AddSamurai(string name)
        {
            // Create a single object to be inserted
            var samurai = new Samurai
            {
                Name = name
            };

            // Add object(s) to be tracked by context
            // Specify target table and data to be added
            context.Samurais.Add(samurai);

            // Push changes to DB
            context.SaveChanges();
        }

        public void AddSamurai(Samurai samurai)
        {
            context.Add(samurai);
            context.SaveChanges();
        }

        public void AddSamurais(List<Samurai> samurais)
        {
            context.AddRange(samurais);
            context.SaveChanges();
        }

        public Samurai GetSamurai(int id, bool fetchAllRelatedData = false)
        {
            Samurai samurai = null;

            if (fetchAllRelatedData)
            {
                // Include all related data in query using LEFT OUTER JOINS.
                samurai = context.Samurais
                    .Include(x => x.Horses)
                    .Include(x => x.Quotes)
                    .Include(x => x.Battles)
                    .FirstOrDefault(x => x.Id == id);
            }
            else
            {
                // Find a single object in a table by id -> No related data
                 samurai = context.Samurais.Find(id);
            }

            return samurai;
        }

        public Samurai GetSamuraiByName(string name)
        {
            // Find a single object in a table by id
            var samurai =
                context.Samurais
                .FirstOrDefault(x => x.Name == name);

            return samurai;
        }

        public List<Samurai> GetSamuraisByName(string name)
        {
            // Find a single object in a table by id
            var samurai =
                context.Samurais
                .Include(x => x.Quotes)
                .Where(x => x.Name == name)
                .ToList();

            return samurai;
        }

        public List<Samurai> GetSamuraiWhereNameContains(string text)
        {
            var samurai = context.Samurais
                .Where(x => x.Name.Contains(text))
                .OrderByDescending(x => x.Name)
                .ToList();

            return samurai;
        }

        public Samurai GetSamuraiWithIncludedData(int id)
        {
            // Using a find(id) does the exact same thing as below
            var samurai = context.Samurais
                .Include(x => x.Horses)
                .Include(x => x.Quotes.Where (y => y.Text.Contains("thank")))
                .FirstOrDefault(x => x.Id == id);

            return samurai;
        }

        public List<Samurai> GetSamurais()
        {
            // Include data in related tables
            return context.Samurais
                .Include(x => x.Quotes)
                .Include(x => x.Horses)
                .ToList();
        }

        public void UpdateSamurai(Samurai samurai)
        {
            context.SaveChanges();
        }

        public void UpdateSamurais()
        {
            // Get samurais -> Skip the first four rows, then take three
            List<Samurai> samurais = context.Samurais
                .Skip(1)
                .Take(6)
                .ToList();

            int i = 0;
            foreach (var samurai in samurais)
            {
                i++;
                samurai.Name = "I was changed in DB " + i;
                samurai.Dynasty = "Sengoku";
            }

            context.SaveChanges();
        }

        public void GetSamuraiWithSql()
        {
            string sql = @"SELECT * FROM USers Where UserId = @id";

        }

        public void DeleteSamurai(int id)
        {
            Samurai samurai = GetSamurai(id);
            context.Samurais.Remove(samurai);
            context.SaveChanges();
        }

        public void AddDifferentObjectsToContext()
        {
            // Objects can be inserted in multiple tables in one statement
            var quote = new Quote
            {
                SamuraiId = 1,
                Text = "If the Bird does not sing, Kill it."
            };

            var horse = new Horse
            {
                SamuraiId = 1,
                Age = 5,
                IsWarHorse = true,
                Name = "Jolly jumper"
            };

            context.Add(quote);
            context.Add(horse);
            context.SaveChanges();
        }

        Samurai ISamuraiRepo.GetSamuraiWhereNameContains(string text)
        {
            throw new NotImplementedException();
        }

        public List<Samurai> GetResultFromStoredProcedure(string text)
        {
            var samurais = context.Samurais.FromSqlRaw(
                "EXEC [dbo].[SamuraisWhoSaidAWord] {0}", text)
                .ToList();

            return samurais;
        }
    }
}