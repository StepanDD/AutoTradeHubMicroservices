using AutoTradeHubDataWebApi.Data;
using AutoTradeHubDataWebApi.Interfaces;
using AutoTradeHubDataWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoTradeHubDataWebApi.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Car car)
        {
            _context.Add(car);
            return Save();
        }

        public bool Delete(Car car)
        {
            _context.Remove(car);
            return Save();
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.cars.Include(a => a.Marka).Include(a => a.Model).ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.cars.Include(a => a.Color).Include(a => a.Generation).Include(a => a.Marka).Include(a => a.Model).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Car>> GetCarByMarka(string marka)
        {
            return await _context.cars.Include(a => a.Marka).Include(a => a.Model).Where(c => c.Marka.Name.Contains(marka)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Car car)
        {
            _context.Update(car);
            return Save();
        }
    }
}
