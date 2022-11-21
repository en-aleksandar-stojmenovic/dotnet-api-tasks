using ProjectSetup.Contracts.V1;
using ProjectSetup.Data;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private ApplicationDbContext _context;
		private IPostRepository _postRepo;

		public RepositoryWrapper(ApplicationDbContext context)
		{
			_context = context;
		}
		public IPostRepository Post 
		{
			get
			{
				if(_postRepo == null)
				{
					_postRepo = new PostRepository(_context);
				}

				return _postRepo;
			}
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
