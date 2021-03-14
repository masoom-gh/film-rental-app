using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudio.Authentication
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Studio Name is required")]
        public string StudioName { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Name of the chairman is required")]
        public string ChairmanName { get; set; }

        [Required(ErrorMessage = "Mobile number of chairman is required")]
        public string ChairmanMobileNumber { get; set; }


    }
}
