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
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Display(Name = "Заглавие")]
        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Title must be between 12 and 100!")]
        public string Title { get; set; }

        [Display(Name = "Спорт")]
        [Required(ErrorMessage = "Sport is required!")]
        public Sports Sport { get; set; }

        [Display(Name = "Търсена позиция")]
        [Required(ErrorMessage = "Must have searched position!")]
        public Position SearchedPosition { get; set; }

        [Display(Name = "Търсен силен крак/ръка")]
        [Required(ErrorMessage = "Must have searched strong foot!")]
        public LeftOrRightFoot? SearchedStrongFoot { get; set; }

        [Display(Name = "Минимум години")]
        [Required(ErrorMessage = "Minimum age is required!")]
        [Range(12, 40, ErrorMessage = "Minimum age must be between 12 and 40!")]
        public int MinimumAge { get; set; }

        [Display(Name = "Максимум години")]
        [Required(ErrorMessage = "Maximum age is required!")]
        [Range(13, 41, ErrorMessage = "Maximum age must be between 12 and 41!")]
        public int MaximumAge { get; set; }

        [Display(Name = "Описание")]
        [StringLength(5000, MinimumLength = 20, ErrorMessage = "Description must be between 20 and 5000 words")]
        public string Description { get; set; }

        [Display(Name = "Дата")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Потребител")]
        public string UserId { get; set; }

        [Display(Name = "Потребител")]
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

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
