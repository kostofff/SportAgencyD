using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public Role UserRole { get; set; }
        public virtual ICollection<AthleteAd> AthleteAds { get; set; }
        public virtual ICollection<ClubAd> ClubAds { get; set; }

        public User()
        {
            AthleteAds = new List<AthleteAd>();
            ClubAds = new List<ClubAd>();
            Id = Guid.NewGuid().ToString();
        }
        public User(string userName, string email, string phoneNumber, Role userRole)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
        }
    }
}
