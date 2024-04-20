using AutoTradeHubDataWebApi.Data;
using AutoTradeHubDataWebApi.Interfaces;
using AutoTradeHubDataWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoTradeHubDataWebApi.Repository
{
    public class ModelRepository : IModelRepository
    {
        private readonly AppDbContext _context;

        public ModelRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Model model)
        {
            _context.Add(model);
            return Save();
        }

        public bool Delete(Model model)
        {
            _context.Remove(model);
            return Save();
        }

        public async Task<IEnumerable<Model>> GetAll()
        {
            return await _context.models.Include(a => a.Marka).ToListAsync();
        }

        public async Task<Model> GetByIdAsync(int id)
        {
            return await _context.models.Include(a => a.Marka).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Model>> GetByMarkaAsync(string marka)
        {
            return await _context.models.Include(a => a.Marka).Where(c => c.Marka.Name.Contains(marka)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Model model)
        {
            _context.Update(model);
            return Save();
        }
    }
}
