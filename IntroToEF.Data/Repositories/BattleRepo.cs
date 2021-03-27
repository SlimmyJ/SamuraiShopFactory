using IntroToEF.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToEF.Data.Repositories
{
    public class BattleRepo : IBattleRepo
    {
        private SamuraiContext _context;

        public BattleRepo()
        {
            _context = new SamuraiContext();
        }

        public List<Battle> GetAllBattles()
        {
            return _context.Battles.ToList();
        }

        public Battle FindBattleByID(int id)
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
