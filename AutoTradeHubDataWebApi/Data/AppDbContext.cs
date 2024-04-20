using AutoTradeHubDataWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoTradeHubDataWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Car> cars { get; set; }
		public DbSet<Marka> marks { get; set; }
		public DbSet<Model> models { get; set; }
		public DbSet<Generation> generations { get; set; }
		public DbSet<Color> colors { get; set; }
	}
}
