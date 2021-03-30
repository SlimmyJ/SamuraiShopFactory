using System.Collections.Generic;

using IntroToEF.Data.Entities;

namespace IntroToEF.Data.Repositories
    {
    public interface IBattleRepo
        {
        List<Battle> GetAllBattles();

        void UpdateBattle(Battle battle);

        Battle FindBattleById(int id);

        void AddBattle(string name, int year, string place);

        List<Samurai> GetSamuraisThatFoughtInASpecificBattle(int userId);
        }
    }