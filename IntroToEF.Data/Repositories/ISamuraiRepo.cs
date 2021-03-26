using IntroToEF.Data.Entities;
using System.Collections.Generic;

namespace IntroToEF.Data.Repositories
{
    public interface ISamuraiRepo
    {
        void AddDifferentObjectsToContext();
        void AddSamurai(Samurai samurai);
        void AddSamurai(string name);
        void AddSamurais(List<Samurai> samurais);
        void DeleteSamurai(int id);
        Samurai GetSamurai(int id, bool fetchAllRelatedData = false);
        Samurai GetSamuraiWithIncludedData(int id);
        List<Samurai> GetSamurais();
        void UpdateSamurai(Samurai samurai);
        void UpdateSamurais();
        Samurai GetSamuraiWhereNameContains(string text);
        List<Samurai> GetResultFromStoredProcedure(string text);
    }
}