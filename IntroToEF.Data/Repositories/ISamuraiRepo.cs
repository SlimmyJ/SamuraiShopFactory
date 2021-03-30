using System.Collections.Generic;

using IntroToEF.Data.Entities;

namespace IntroToEF.Data.Repositories
    {
    public interface ISamuraiRepo
        {
        void AddDifferentObjectsToContext();

        void AddSamurai(Samurai samurai);

        void AddSamurai(string name, string dynasty);

        void AddSamurais(List<Samurai> samurais);

        void DeleteSamurai(int id);

        List<Samurai> GetResultFromStoredProcedure(string text);

        Samurai GetSamurai(int id, bool fetchAllRelatedData = false); //CHECK THIS

        Samurai GetSamuraiByName(string name);

        List<Samurai> GetSamurais();

        List<Samurai> GetSamuraisByName(string name);

        List<Samurai> GetSamuraiWhereNameContains(string text);

        Samurai GetSamuraiWithIncludedData(int id);

        void GetSamuraiWithSql();

        void UpdateSamurai(Samurai samurai);

        void UpdateSamurais();
        }
    }