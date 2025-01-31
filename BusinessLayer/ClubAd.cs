using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ClubAd:Ad
    {
        #region Properties

        [Required(ErrorMessage = "Must have searched position!")]
        public Position SearchedPosition { get; set; }
        [Required(ErrorMessage = "Must have searched strong foot!")]
        public LeftOrRightFoot SearchedStrongFoot { get; set; }
        [Required(ErrorMessage = "Minimum age is required!")]
        [Range(7,44,ErrorMessage ="Minimum age must be between 7 and 44!")]
        public int MinimumAge { get; set; }
        [Required(ErrorMessage = "Maximum age is required!")]
        [Range(8, 45, ErrorMessage = "Maximum age must be between 8 and 45!")]
        public int MaximumAge { get; set; }
        [StringLength(2000,MinimumLength =30,ErrorMessage ="Description must be between 30 and 2000 words")]
        public string Description { get; set; }
        #endregion

        #region Constructor
        public ClubAd()
        { 
        
        }
        public ClubAd(string title,string userId,Position searchedPosition,LeftOrRightFoot searchedStrongFoot,int minimumAge
            ,int maximumAge,string description):base(title,userId) 
        { 
         SearchedPosition = searchedPosition;
         SearchedStrongFoot = searchedStrongFoot;
         MinimumAge = minimumAge;
         MaximumAge = maximumAge;
         Description = description;
        }
        #endregion
    }
}
