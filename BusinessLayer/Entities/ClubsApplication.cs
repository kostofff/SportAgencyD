using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class ClubsApplication
    {

        public string ApplicationId { get; set; } = Guid.NewGuid().ToString();

        public string ClubId { get; set; }
        [Display(Name = "Клуб")]
        public Club Club { get; set; }

        public string AthleteId { get; set; }
        [Display(Name = "Атлет")]
        public Athlete Athlete { get; set; }

        public string AthleteAdId { get; set; }
        [Display(Name = "Обява")]
        public AthleteAd AthleteAd { get; set; }

        [Display(Name = "Статус")]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        [Display(Name = "Дата на създаване")]
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
