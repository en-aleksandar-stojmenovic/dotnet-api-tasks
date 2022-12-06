using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
		private readonly IConfiguration _config;

		public RepositoryWrapper(ApplicationDbContext context, UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_config = config;
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
					_userRepo = new UserRepository(_context, _userManager, _roleManager, _config);
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
