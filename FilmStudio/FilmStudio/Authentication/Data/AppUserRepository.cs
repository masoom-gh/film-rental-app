using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmStudio.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmStudio.Authentication
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _identityContext;

        public AppUserRepository(AppDbContext identityContext)
        {
            _identityContext = identityContext;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            var result = _identityContext.Users.Where(u => u.StudioName != null);
            return result;
        }

        public ApplicationUser GetByUsername(string username)
        {
            return _identityContext.Users.Where(u => u.UserName == username).FirstOrDefault();
        }
    }
}
