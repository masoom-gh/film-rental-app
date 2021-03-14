using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FilmStudio.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string StudioName { get; set; }
        public string City { get; set; }
        public string ChairmanName { get; set; }
        public string ChairmanMobileNumber { get; set; }
    }
}
