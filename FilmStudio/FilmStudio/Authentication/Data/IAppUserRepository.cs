using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudio.Authentication
{
    public interface IAppUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser GetByUsername(string username);
    }
}
