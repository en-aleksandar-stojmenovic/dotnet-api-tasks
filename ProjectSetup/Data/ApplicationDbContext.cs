using Microsoft.EntityFrameworkCore;
using ProjectSetup.Domain;

namespace ProjectSetup.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Post> Posts { get; set; }
	}
}
