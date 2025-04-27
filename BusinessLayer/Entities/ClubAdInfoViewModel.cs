using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class ClubAdInfoViewModel
    {
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string AdId { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Търсен Спорт")]
        public string AdSport { get; set; }
        [Display(Name = "Търсена Позиция")]
        public string AdPosition { get; set; }
        [Display(Name = "Търсен силен крак/ръка")]
        public LeftOrRightFoot LeftOrRightFoot { get; set; }
        [Display(Name = "Минимум години")]
        public int MinimumAge { get; set; }
        [Display(Name = "Максимум години")]
        public int MaximumAge { get; set; }
    }
}
