using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class AthleteAd
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Must have position!")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Must have Country!")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Must have City!")]
        public string City { get; set; }

        //public int Age { get; set; }
        [Required(ErrorMessage = "Must choose left or rigt foot!")]
        public string LeftOrRighFoot { get; set; }

        public string TeamsPlayed { get; set; }
        public string Achievements { get; set; }
      //public string PhoneNumber { get; set; }
      //public string Email { get; set; }
        
        public string AthleteId { get; set; }
        public Athlete Athlete { get; set; }

        public AthleteAd(Athlete athlete,string postion,string country,string city,string leftOrRightFoot,string teamsPlayed,string achievements) 
        { 
         Athlete = athlete;
         AthleteId = athlete.UserId;
         Position = postion;
         Country = country;
         City = city;
         LeftOrRighFoot = leftOrRightFoot;
         TeamsPlayed = teamsPlayed;
         Achievements = achievements;
        }

    }
}
