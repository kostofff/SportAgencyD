using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public enum Position
    { 
     GoalKeeper,CentralBack,LeftBack,RightBack
    ,CentralDefensiveMidfielder,CentralMidfielder,CentralAtackingMidfielder,LeftMidfielder,RightMidfielder
    ,LeftWinger,RightWinger,Striker
    }
    public enum LeftOrRightFoot
    { 
     LeftFoot,
     RightFoot
    }
    public class AthleteAd:Ad
    {
        #region Properties
        [Required(ErrorMessage = "Position is required!")]
        public Position Position { get; set; }
        [Required(ErrorMessage = "Country is required!")]
        public Country Country { get; set; }
        [Required(ErrorMessage = "City is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "City name must be between 5 and 50!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Must choose between left or rigt foot!")]
        public LeftOrRightFoot LeftOrRighFoot { get; set; }
        [Required(ErrorMessage = "Teams that you played is required!")]
        [StringLength(5000, MinimumLength = 5, ErrorMessage = "Max 5000 symbols")]
        public string TeamsPlayed { get; set; }
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Must be between 5 and 1000!")]
        public string Achievements { get; set; }

        #endregion

        #region Constructors

        public AthleteAd()
        { 
         
        }
        public AthleteAd(string title,string userId,Position postion,Country country,string city, LeftOrRightFoot leftOrRightFoot
            ,string teamsPlayed,string achievements):base(title,userId) 
        { 
         Position = postion;
         Country = country;
         City = city;
         LeftOrRighFoot = leftOrRightFoot;
         TeamsPlayed = teamsPlayed;
         Achievements = achievements;
        }

        #endregion

    }
}
