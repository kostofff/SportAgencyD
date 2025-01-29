using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ClubAd
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Must have searched position!")]
        public string SearchedPosition { get; set; }
        [Required(ErrorMessage = "Must have searched strong foot!")]
        public string SearchedStrongFoot { get; set; }

        public string MinimumAge { get; set; }
        public string MaximumAge { get; set; }
        public string Description { get; set; }
      //public string PhoneNumber { get; set; }
      //public string Email { get; set; }
        public string ClubId { get; set; }
        public Club Club { get; set; }
        public ClubAd(Club club,string searchedPosition,string searchedStrongFoot,string minimumAge,string maximumAge,string description) 
        { 
         Club = club;
         ClubId = club.UserId;
         SearchedPosition = searchedPosition;
         SearchedStrongFoot = searchedStrongFoot;
         MinimumAge = minimumAge;
         MaximumAge = maximumAge;
         Description = description;
        }
    }
}
