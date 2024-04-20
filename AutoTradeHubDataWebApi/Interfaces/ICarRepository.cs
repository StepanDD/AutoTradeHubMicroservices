using AutoTradeHubDataWebApi.Models;

namespace AutoTradeHubDataWebApi.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task<Car> GetByIdAsync(int id);
        Task<IEnumerable<Car>> GetCarByMarka(string marka);
        bool Add(Car car);
        bool Update(Car car);
        bool Delete(Car car);
        bool Save();
    }
}
