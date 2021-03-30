using System.Collections.Generic;
using System.Linq;

using IntroToEF.Data.Entities;

namespace IntroToEF.Data.Repositories
    {
    public class BattleRepo : IBattleRepo
        {
        private SamuraiContext _context;

        public BattleRepo()
            {
            _context = new SamuraiContext();
            }

        public List<Samurai> GetSamuraisThatFoughtInASpecificBattle(int battleId)
            {
            var mySamurai = new List<Samurai>();
            foreach (var samurai in _context.Battles.Where(x => x.Id == battleId)
                .SelectMany(y => y.Samurais))
                mySamurai.Add(samurai);
            return mySamurai;
            }

        public List<Battle> GetAllBattles()
            {
            return _context.Battles.ToList();
            }

        public Battle FindBattleById(int id)
            {
            return _context.Battles.Find(id);
            }

        public void UpdateBattle(Battle battle)
            {
            _context.SaveChanges();
            }

        public void AddBattle(string name, int year, string place)
            {
            var battle = new Battle
                {
                Name = name,
                Year = year,
                Location = place,
                };
            _context.Battles.Add(battle);
            _context.SaveChanges();
            }
        }
    }