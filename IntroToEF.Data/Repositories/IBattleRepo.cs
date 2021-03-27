using IntroToEF.Data.Entities;
using System.Collections.Generic;

namespace IntroToEF.Data.Repositories
{
    public interface IBattleRepo
    {
        List<Battle> GetAllBattles();
        void UpdateBattle(Battle battle);
        Battle FindBattleByID(int id);
        void AddBattle(string name, int year, string place);
    }
}