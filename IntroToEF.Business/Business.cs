using System;
using System.Collections.Generic;

using IntroToEF.Data.Entities;
using IntroToEF.Data.Repositories;

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
            PrintMenu();

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
                    PrintSamuraiInfo(AskUserForSamuraiId());
                    break;

                case 4:
                    PrintSamuraisWithHorses();
                    break;

                case 5:
                    PrintSamuraisThatFoughtInASpecificBattle();
                    break;

                case 6:

                    EditSamurai();
                    PrintEditMenu();
                    GetUserInt(6);

                    break;
                }
            }

        private void PrintSamuraiInfo(int id)
            {
            var thisSamurai = FindSamuraiById(id);
            Console.WriteLine($"This samurais name is {thisSamurai.Name}");
            WriteHorses(thisSamurai);
            }

        private int AskUserForSamuraiId()
            {
            Console.WriteLine("Enter the samurai's ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
            }

        private void EditSamurai()
            {
            }

        private static void PrintMenu()
            {
            Console.WriteLine("SAMURAI MENU:");
            Console.WriteLine("1. Create a new Samurai");
            Console.WriteLine("2. Add Horses to a Samurai");
            Console.WriteLine("3. Show samurai info");
            Console.WriteLine("4. See all samurais with horses");
            Console.WriteLine("5. See all samurais that fought in one battle");
            Console.WriteLine("6. Edit Samurai");
            }

        private static int GetUserInt(int top)
            {
            var v = Console.ReadLine();
            if (v != null)
                {
                var usersChoice = int.Parse(s: v);
                if (usersChoice <= top) return usersChoice;
                PrintEditMenu();
                return 0;
                }

            Console.WriteLine("Invalid Command");
            return 0;
            }

        private static void PrintEditMenu()
            {
            Console.WriteLine("EDIT SAMURAI MENU:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Dynasty");
            Console.WriteLine("3. Quotes");
            Console.WriteLine("4. Horses");
            Console.WriteLine("5. Battles");
            Console.WriteLine("6. SAVE");
            }

        private void PrintSamuraisThatFoughtInASpecificBattle()
            {
            Console.WriteLine("Give a Battle ID:");
            var userBattleId = Convert.ToInt32(Console.ReadLine());
            var samuraisInBattles = _battle.GetSamuraisThatFoughtInASpecificBattle(userBattleId);
            Console.WriteLine("The Samurais that fought in this battle are:");
            foreach (var mySamurai in samuraisInBattles)
                {
                Console.WriteLine(mySamurai.Name);
                }
            }

        private void PrintSamuraisWithHorses()
            {
            var samurais = FindAllSamuraisWithHorses();
            foreach (var samurai in samurais)
                {
                Console.WriteLine("Samurais without Horses: ");
                Console.WriteLine(samurai.Name);
                }
            }

        private void GiveAHorseToASamurai()
            {
            var id = AskUserForSamuraiId();
            var sum = FindSamuraiById(id);

            Console.WriteLine("Give a name of the Horse: ");

            var newHorseName = Console.ReadLine();

            sum.Horses.Add(new Horse
                {
                Name = newHorseName
                });

            _repo.UpdateSamurai(sum);
            }

        private static void WriteHorses(Samurai thisSamurai)
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
            Console.WriteLine($"Name: {sum.Name} Dynasty: {sum.Dynasty} found.");
            return sum;
            }

        private List<Samurai> FindAllSamuraisWithHorses()
            {
            List<Samurai> allSamurais = GetAllSamurais();
            var samuraisWithHorses = new List<Samurai>();

            foreach (var samurai in allSamurais)
                {
                if (samurai.Horses.Count > 0)
                    {
                    samuraisWithHorses.Add(samurai);
                    }
                }
            return samuraisWithHorses;
            }

        public void RemoveSamurai(int id)
            {
            _repo.DeleteSamurai(id);
            }

        public List<Samurai> GetAllSamurais()
            {
            return _repo.GetSamurais();
            }
        }
    }