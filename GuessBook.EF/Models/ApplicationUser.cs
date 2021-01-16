using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessBook.EF.Models
{
    public class ApplicationUser : IdentityUser
    {
        public long? NationalIdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool UserIsActive { get; set; }
        public string UserDetail { get; set; }
        public string UserAddress { get; set; }
        public string UserImageUrl { get; set; }
        //public bool IsAdmin { get; set; }

    }
}
