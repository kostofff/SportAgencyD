using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Athlete:User
    {
        #region Properties
        [Required(ErrorMessage ="First name is required!")]
        [StringLength(50,MinimumLength = 2,ErrorMessage ="First name must be between 2 and 50!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Age is required!")]
        [Range(7,45,ErrorMessage ="Your age must be between 7 and 45!")]
        public int Age { get; set; }
        #endregion

        #region Constructors
        public Athlete()
        { 
        
        }
        public Athlete(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        #endregion
    }
}
