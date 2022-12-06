using Microsoft.AspNetCore.Identity;
using ProjectSetup.Data;
using ProjectSetup.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IPostRepository _postRepo;
		private IUserRepository _userRepo;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public RepositoryWrapper(ApplicationDbContext context, UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager)
        {
            _context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}
        public IPostRepository Post
        {
            get
            {
                if (_postRepo == null)
                {
                    _postRepo = new PostRepository(_context);
                }

                return _postRepo;
            }
        }

		public IUserRepository User
		{
			get
			{
				if (_userRepo == null)
				{
					_userRepo = new UserRepository(_context, _userManager, _roleManager);
				}

				return _userRepo;
			}
		}

		public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
