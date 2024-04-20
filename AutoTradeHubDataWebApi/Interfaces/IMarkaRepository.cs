using AutoTradeHubDataWebApi.Models;

namespace AutoTradeHubDataWebApi.Interfaces
{
    public interface IMarkaRepository
    {
        Task<IEnumerable<Marka>> GetAll();
        Task<Marka> GetByIdAsync(int id);
        bool Add(Marka marka);
        bool Update(Marka marka);
        bool Delete(Marka marka);
        bool Save();
    }
}
