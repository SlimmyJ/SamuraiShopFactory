using IntroToEF.Data.Entities;
using IntroToEF.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Threading.Channels;
using System.Xml;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace IntroToEF.Business
    {
    public class
        Business
        {
        // Composition
        private ISamuraiRepo _repo;

        public Business()
            {
            _repo = new SamuraiRepo();
            }

        public void RunApp()
            {
            Console.WriteLine("What?");
            Console.WriteLine("1. Create a new Samurai");
            Console.WriteLine("2. Add Horses to a Samurai");
            Console.WriteLine("3. Show samurai info");
            Console.WriteLine("4. See all samurais with horses");

            int userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
                {
                case 1:
                    CreateANewSamurai();
                    break;

                case 2:
                    Console.WriteLine("Give Samurasi ID: ");
                    int samuraiID = Convert.ToInt32(Console.ReadLine());
                    var sum = FindSamuraiById(samuraiID);
                    Console.WriteLine("Give a name of the Horse: ");
                    string name = Console.ReadLine();
                    sum.Horses.Add(new Horse
                        {
                        Name = name
                        });

                    _repo.UpdateSamurai(sum);
                    break;

                case 3:
                    Console.WriteLine("Enter the samurai's ID:");
                    int userinput = Convert.ToInt32(Console.ReadLine());
                    var thissamurai = FindSamuraiById(userinput);

                    Console.WriteLine($"This samurais name is {thissamurai.Name}");
                    Writehorses(thissamurai);

                    break;

                case 4:
                    Console.WriteLine();
                    break;

                default:
                    break;
                }
            }

        private void Writehorses(Samurai thissamurai)
            {
            foreach (Horse thisguyshorses in thissamurai.Horses)
                {
                Console.WriteLine(thisguyshorses.Name);
                }
            }

        private void CreateANewSamurai()
            {
            Console.WriteLine("Hello. Please enter a name for your Samurai:");
            string name = Console.ReadLine();
            Console.WriteLine("Dynasty of your Samurai");
            string dynasty = Console.ReadLine();
            _repo.AddSamurai(name, dynasty);
            Console.ReadLine();
            }

        private Samurai FindSamuraiById(int id)
            {
            Samurai sum = _repo.GetSamurai(id, true);
            Console.WriteLine($"Name: {sum.Name} Dynasty: {sum.Dynasty}");
            return sum;
            }

        public List<Samurai> GetSamuraiWhoSaidAWord(string word)
            {
            var result = _repo.GetResultFromStoredProcedure(word);
            return result;
            }

        public void RemoveSamurai(int id)
            {
            _repo.DeleteSamurai(id);
            }

        public void AddSamuraiWithHorses()
            {
            var samurai = new Samurai
                {
                Name = "Samurai With Horses",
                Dynasty = "Sengoku",
                Horses = new List<Horse>
                {
                    new Horse
                    {
                        IsWarHorse = true,
                        Name = "Roach"
                    },
                    new Horse
                    {
                        IsWarHorse = false,
                        Name = "Boeddika"
                    }
                }
                };

            _repo.AddSamurai(samurai);
            }

        public void AddSamuraiWhoFoughtInBattles()
            {
            Samurai veteran = new Samurai
                {
                Name = "A weary broken man",
                Battles = new List<Battle>
                {
                    new Battle
                    {
                        Name = "Okinagawa",
                        Year = 1557
                    },
                    new Battle
                    {
                        Name = "Fukushima",
                        Year = 2011
                    }
                }
                };

            _repo.AddSamurai(veteran);
            }

        public void GetAllSamurais()
            {
            var samurais = _repo.GetSamurais();
            }

        public void RenameSamurai(int id, string name)
            {
            // Get element from DB
            Samurai samuraiToBeUpdated = _repo.GetSamurai(id);

            // Perform changes
            samuraiToBeUpdated.Name = name;

            // Save object back to db
            _repo.UpdateSamurai(samuraiToBeUpdated);
            }

        public void RenameMultipleSamurais()
            {
            // Bad practice -> Code in datalayer should go here.
            _repo.UpdateSamurais();
            }

        public Samurai GetSamuraiWithBattles(int id)
            {
            return _repo.GetSamurai(id, true);
            }
        }
    }