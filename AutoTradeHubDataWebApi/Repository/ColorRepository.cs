using AutoTradeHubDataWebApi.Data;
using AutoTradeHubDataWebApi.Interfaces;
using AutoTradeHubDataWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoTradeHubDataWebApi.Repository
{
    public class ColorRepository : IColorRepository
    {
        private readonly AppDbContext _context;

        public ColorRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Color color)
        {
            _context.Add(color);
            return Save();
        }

        public bool Delete(Color color)
        {
            _context.Remove(color);
            return Save();
        }

        public async Task<IEnumerable<Color>> GetAll()
        {
            return await _context.colors.ToListAsync();
        }

        public async Task<Color> GetByIdAsync(int id)
        {
            return await _context.colors.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Color color)
        {
            _context.Update(color);
            return Save();
        }
    }
}
