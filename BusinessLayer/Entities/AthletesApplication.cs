using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public enum ApplicationStatus
    {
        Pending,
        Accepted,
        Rejected
    }
    public class AthletesApplication
    {
        public string ApplicationId { get; set; } = Guid.NewGuid().ToString();

        public string AthleteId { get; set; }
        public Athlete Athlete { get; set; }

        public string ClubId { get; set; }
        public Club Club { get; set; }

        public string ClubAdId { get; set; }
        public ClubAd ClubAd { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public AthletesApplication() { }

        public AthletesApplication(Athlete athlete, Club club, ClubAd clubAd)
        {
            Athlete = athlete;
            AthleteId = athlete.Id;
            Club = club;
            ClubId = club.Id;
            ClubAd = clubAd;
            ClubAdId = clubAd.Id;
        }
    }

}
