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
       public Role UserRole { get; set; } 

        public User() 
        { 
        
        }
        public User(string userName, string email, Role userRole) 
        { 
         UserName = userName;
         Email = email;
         UserRole = userRole;
        }
    }
}
