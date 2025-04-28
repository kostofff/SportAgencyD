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
        #region Properties

        public string ApplicationId { get; set; } = Guid.NewGuid().ToString();

        public string AthleteId { get; set; }
        [Display(Name = "Атлет")]
        public Athlete Athlete { get; set; }

        public string ClubId { get; set; }
        [Display(Name = "Клуб")]
        public Club Club { get; set; }

        public string ClubAdId { get; set; }
        [Display(Name = "Обява")]
        public ClubAd ClubAd { get; set; }
        [Display(Name = "Статус")]

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        [Display(Name = "Дата на създаване")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        #endregion

        #region Constructors
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

        #endregion
    }

}
