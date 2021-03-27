using System;
using System.Collections.Generic;
using System.Linq;

using IntroToEF.Data.Entities;
using IntroToEF.Data.Repositories;

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
            ShowMenu();

            var userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
                {
                case 1:
                        {
                        CreateANewSamurai();
                        break;
                        }

                case 2:
                        {
                        Samurai samuraiFoundById = GetSamuraiConsole();
                        AddHorseToSamurai(samuraiFoundById);
                        _repo.UpdateSamurai(samuraiFoundById);

                        break;
                        }

                case 3:
                        {
                        Console.WriteLine("Enter the samurai's ID:");
                        var userinput = Convert.ToInt32(Console.ReadLine());
                        Samurai thissamurai = FindSamuraiById(userinput);

                        Console.WriteLine($"This samurais name is {thissamurai.Name}");
                        Writehorses(thissamurai);

                        break;
                        }

                case 4:
                        {
                        List<Samurai> allSamurai = _repo.GetSamurais();

                        foreach (Samurai thisdude in allSamurai.Where(thisdude => thisdude.Horses.Count > 0))
                            {
                            Console.WriteLine(".........SAMURAI...........");
                            Console.WriteLine("...........................");
                            Console.WriteLine(thisdude.Name);
                            Console.WriteLine("...........................");
                            Console.WriteLine(".........HORSES............");

                            foreach (Horse horse in thisdude.Horses) Console.WriteLine(horse.Name);

                            Console.WriteLine("...........................");
                            }

                        break;
                        }

                default:
                    break;
                }

            Console.ReadKey();
            }

        private void AddHorseToSamurai(Samurai samurai)
            {
            Console.WriteLine("Give type the name of the horse you wish to add: ");
            string nameOfNewHorse = Console.ReadLine();
            var newHorse = new Horse { Name = nameOfNewHorse };
            samurai.Horses.Add(newHorse);
            }

        private Samurai GetSamuraiConsole()
            {
            Console.WriteLine("Give Samurais ID: ");
            var samuraiId = Convert.ToInt32(Console.ReadLine());
            Samurai samuraiFoundById = FindSamuraiById(samuraiId);
            return samuraiFoundById;
            }

        private static void ShowMenu()
            {
            Console.WriteLine("What?");
            Console.WriteLine("1. Create a new Samurai");
            Console.WriteLine("2. Add Horses to a Samurai");
            Console.WriteLine("3. Show samurai info");
            Console.WriteLine("4. See all samurais with horses");
            }

        private void Writehorses(Samurai thissamurai)
            {
            foreach (Horse thisguyshorses in thissamurai.Horses) Console.WriteLine(thisguyshorses.Name);
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

        public void UpdateSamurais()
            {
            _repo.UpdateSamurais();
            }
        }
    }