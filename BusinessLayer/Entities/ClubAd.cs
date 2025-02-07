using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class ClubAd
    {
        #region Properties
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Title must be between 12 and 100!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Sport is required!")]
        public Sports Sport { get; set; }

        [Required(ErrorMessage = "Must have searched position!")]
        public Position SearchedPosition { get; set; }

        [Required(ErrorMessage = "Must have searched strong foot!")]
        public LeftOrRightFoot SearchedStrongFoot { get; set; }

        [Required(ErrorMessage = "Minimum age is required!")]
        [Range(12, 40, ErrorMessage = "Minimum age must be between 12 and 40!")]
        public int MinimumAge { get; set; }

        [Required(ErrorMessage = "Maximum age is required!")]
        [Range(13, 41, ErrorMessage = "Maximum age must be between 12 and 41!")]
        public int MaximumAge { get; set; }

        [StringLength(5000, MinimumLength = 20, ErrorMessage = "Description must be between 20 and 5000 words")]
        public string Description { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        #endregion

        #region Constructor
        public ClubAd()
        {

        }
        public ClubAd(User user, string title,Sports sport, Position searchedPosition, LeftOrRightFoot searchedStrongFoot, int minimumAge
            , int maximumAge, string description)
        {
            User = user;
            UserId = user.Id;
            Title = title;
            Sport = sport;
            SearchedPosition = searchedPosition;
            SearchedStrongFoot = searchedStrongFoot;
            MinimumAge = minimumAge;
            MaximumAge = maximumAge;
            Description = description;
        }
        #endregion
    }
}
