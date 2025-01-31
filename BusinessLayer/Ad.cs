using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Ad
    {
        #region Properties
        [Key]
        public string Id { get; set; }
        [Required]
        public AdType AdType { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Title must be between 2 and 50!")]
        public string Title { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        #endregion

        #region Constructors
        public Ad()
        { 
        
        }

        public Ad(string title,string userId) 
        { 
         Title = title;
         UserId = userId;
        }

        #endregion
    }
}
