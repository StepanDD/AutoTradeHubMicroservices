using AutoTradeHubDataWebApi.Models;

namespace AutoTradeHubDataWebApi.Interfaces
{
    public interface IGenerationRepository
    {
        Task<IEnumerable<Generation>> GetAll();
        Task<Generation> GetByIdAsync(int id);
        Task<IEnumerable<Generation>> GetByModelAsync(string model);
        bool Add(Generation generation);
        bool Update(Generation generation);
        bool Delete(Generation generation);
        bool Save();
    }
}
