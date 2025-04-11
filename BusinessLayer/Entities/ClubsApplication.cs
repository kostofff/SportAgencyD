using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class ClubsApplication
    {

        public string ApplicationId { get; set; } = Guid.NewGuid().ToString();

        public string ClubId { get; set; }
        public Club Club { get; set; }

        public string AthleteId { get; set; }
        public Athlete Athlete { get; set; }

        public string AthleteAdId { get; set; }
        public AthleteAd AthleteAd { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ClubsApplication() { }

        public ClubsApplication(Athlete athlete, Club club, AthleteAd clubAd)
        {
            Athlete = athlete;
            AthleteId = athlete.Id;
            Club = club;
            ClubId = club.Id;
            AthleteAd = clubAd;
            AthleteAdId = clubAd.Id;
        }
    }
}
