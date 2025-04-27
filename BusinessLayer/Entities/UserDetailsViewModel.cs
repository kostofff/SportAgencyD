using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class UserDetailsViewModel
    {
        // Полета за всички потребители
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserRole { get; set; }

        // Полета за атлети
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        // Полета за клубове
        public string ClubName { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string League { get; set; }
        public string Website { get; set; }

    }
}
