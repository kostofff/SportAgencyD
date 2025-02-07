using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public enum Position
    {
        GoalKeeper, CentralBack, LeftBack, RightBack
    , CentralDefensiveMidfielder, CentralMidfielder, CentralAtackingMidfielder, LeftMidfielder, RightMidfielder
    , LeftWinger, RightWinger, Striker
    }
    public enum LeftOrRightFoot
    {
        LeftFoot,
        RightFoot
    }
    public enum Sports
    { 
     Football,Basketball,Voleyball,Tennis,TableTennis,Golf,Athletics,MMA,Judo,Krate,Box,Wrestling,Jiu_Jitsu,Gymnastics,Swimming,ESports
    }
    public class AthleteAd
    {
        #region Properties
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Title must be between 12 and 100!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Sport is required!")]
        public Sports Sport { get; set; }

        [Required(ErrorMessage = "Position is required!")]
        public Position Position { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        public Country Country { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "City name must be between 1 and 150!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Must choose between left or rigt foot!")]
        public LeftOrRightFoot LeftOrRighFoot { get; set; }

        [Required(ErrorMessage = "Teams that you played is required!")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Max 5000 symbols")]
        public string TeamsPlayed { get; set; }

        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Must be between 1 and 5000!")]
        public string Achievements { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [Required]
        public virtual User User { get; set; }

        #endregion

        #region Constructors

        public AthleteAd()
        {

        }
        public AthleteAd(User user, string title,Sports sport, Position postion, Country country, string city, LeftOrRightFoot leftOrRightFoot
            , string teamsPlayed, string achievements)
        {
            User = user;
            UserId = user.Id;
            Title = title;
            Sport = sport;
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
