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

namespace BusinessLayer
{
    public class User : IdentityUser
    {
        [Required]
        public Role UserRole { get; set; } 
        public virtual ICollection<Ad> Ads { get; set; }

        public User() 
        { 
        
        }
        public User(string userName, string email,string phoneNumber, Role userRole) 
        { 
         UserName = userName;
         Email = email;
         PhoneNumber = phoneNumber;
         UserRole = userRole;
         Ads = new List<Ad>();
        }
    }
}
