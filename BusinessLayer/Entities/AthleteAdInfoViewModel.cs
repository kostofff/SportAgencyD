using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class AthleteAdInfoViewModel
    {
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string AdId { get; set; }
        [Display(Name = "Отбори в които е играл")]
        public string AdTeamsPlayed { get; set; }
        [Display(Name = "Спорт")]
        public string AdSport { get; set; }
        [Display(Name = "Позиция")]
        public string AdPosition { get; set; }
        [Display(Name = "Силен крак/ръка")]
        public LeftOrRightFoot LeftOrRightFoot { get; set; }
        [Display(Name = "Държава")]
        public Country Country { get; set; }
        [Display(Name = "Град")]
        public string City { get; set; }
    }
}
