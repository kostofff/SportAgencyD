using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class Athlete : User
    {
        #region Properties
        [Display(Name = "Име")]
        [Required(ErrorMessage = "First name is required!")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 60!")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 60!")]
        public string LastName { get; set; }

        [Display(Name = "Години")]
        [Required(ErrorMessage = "Age is required!")]
        [Range(12, 40, ErrorMessage = "Your age must be between 12 and 40!")]
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
