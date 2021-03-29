using System.Collections.Generic;

using IntroToEF.Data.Entities;

namespace IntroToEF.Data.Repositories
    {
    public interface ISamuraiRepo
        {
        void AddSamurai(string name, string dynasty);

        void AddSamurai(Samurai samurai);

        void AddSamurais(List<Samurai> samurais);

        Battle FindBattleById(int id);

        Samurai GetSamurai(int id, bool fetchAllRelatedData = false);

        Samurai GetSamuraiByName(string name);

        List<Samurai> GetSamuraisByName(string name);

        List<Samurai> GetSamuraiWhereNameContains(string text);

        Samurai GetSamuraiWithIncludedData(int id);

        List<Samurai> GetSamuraiWithBattles(int battleId);

        List<Samurai> GetSamuraiWithHorses(int id);

        List<Samurai> GetSamurais();

        void UpdateSamurai(Samurai samurai);

        void UpdateSamurais();

        void DeleteSamurai(int id);

        void AddDifferentObjectsToContext();

        List<Samurai> GetResultFromStoredProcedure(string text);
        }
    }