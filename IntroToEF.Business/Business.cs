using IntroToEF.Data.Entities;
using IntroToEF.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
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

        private IBattleRepo _battle;

        public Business()
            {
            _repo = new SamuraiRepo();
            _battle = new BattleRepo();
            }

        public void RunApp()
            {
            Console.WriteLine("What?");
            Console.WriteLine("1. Create a new Samurai");
            Console.WriteLine("2. Add Horses to a Samurai");
            Console.WriteLine("3. Show samurai info");
            Console.WriteLine("4. See all samurais with horses");
            Console.WriteLine("5. See all samurais that fought in one battle");

            int userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
                {
                case 1:
                    CreateANewSamurai();
                    break;

                case 2:
                    GiveAHorseToASamurai();
                    break;

                case 3:
                    Console.WriteLine("Enter the samurai's ID:");
                    int userinput = Convert.ToInt32(Console.ReadLine());
                    var thisSamurai = FindSamuraiById(userinput);
                    Console.WriteLine($"This samurais name is {thisSamurai.Name}");
                    Writehorses(thisSamurai);
                    break;

                case 4:
                    ShowSamuraisWithHorses();
                    break;

                case 5:
                    ShowSamuraisThatFoughtInASpecificBattle();
                    break;

                default:
                    break;
                }
            }

        private void ShowSamuraisThatFoughtInASpecificBattle()
            {
            Console.WriteLine("Give a Battle ID:");
            int userBattleID = Convert.ToInt32(Console.ReadLine());
            var samuraisInAbattle = _battle.GetSamuraisThatFoughtInASpecificBattle(userBattleID);
            Console.WriteLine($"The Samurais that fought in  battle are:");
            foreach (var mySamurai in samuraisInAbattle)
                {
                Console.WriteLine(mySamurai.Name);
                }
            }

        private void ShowSamuraisWithHorses()
            {
            var samurais = FindAllSamuraisWithHorses();
            foreach (var samurai in samurais)
                {
                Console.WriteLine("Samurais with Horses: ");
                Console.WriteLine(samurai.Name);
                }
            }

        private void GiveAHorseToASamurai()
            {
            Console.WriteLine("Give Samurai ID: ");
            int samuraiID = Convert.ToInt32(Console.ReadLine());
            var sum = FindSamuraiById(samuraiID);
            Console.WriteLine("Give a name of the Horse: ");
            string name = Console.ReadLine();
            sum.Horses.Add(new Horse
                {
                Name = name
                });

            _repo.UpdateSamurai(sum);
            }

        private void Writehorses(Samurai thisSamurai)
            {
            foreach (Horse thisGuysHorses in thisSamurai.Horses)
                {
                Console.WriteLine(thisGuysHorses.Name);
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

        private List<Samurai> FindAllSamuraisWithHorses()
            {
            List<Samurai> allSamurais = GetAllSamurais();
            return allSamurais.Where(x => x.Horses.Any()).ToList();
            }

        private List<Samurai> FindAllSamuraisThatFoughtInABattle(int BattleId)
            {
            var battle = _battle.FindBattleByID(BattleId);
            List<Samurai> allSamurais = GetAllSamurais();

            return allSamurais.Where(samurai => samurai.Battles.Contains(battle)).ToList();
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

        public List<Samurai> GetAllSamurais()
            {
            return _repo.GetSamurais();
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