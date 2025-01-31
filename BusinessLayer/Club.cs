using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public enum Country
    {
        Afghanistan, Albania, Algeria, Andorra, Angola, AntiguaandBarbuda, Argentina, Armenia, Australia
            , Austria, Azerbaijan, Bahamas, Bahrain, Bangladesh, Barbados, Belarus, Belgium, Belize, Benin
            , Bhutan, Bolivia, BosniaandHerzegovina, Botswana, Brazil, Brunei, Bulgaria, BurkinaFaso, Burundi
            , CapeVerde, Cambodia, Cameroon, Canada, CentralAfricanRepublic, Chad, Chile, China, Colombia
            , ComorosIslands, Congo, CostaRica, IvoryCoast, Croatia, Cuba, Cyprus
            , CzechRepublic, Denmark, Djibouti, Dominica, DominicanRepublic, EastTimor, Ecuador, Egypt
            , ElSalvador, EquatorialGuinea, Eritrea, Estonia, Eswatini , Ethiopia, Fiji, Finland
            , France, Gabon, Gambia, Georgia, Germany, Ghana, Greece, Grenada, Guatemala, Guinea, GuineaBissau
            , Guyana, Haiti, Honduras, Hungary, Iceland, India, Indonesia, Iran, Iraq, Ireland, Israel, Italy
            , Jamaica, Japan, Jordan, Kazakhstan, Kenya, Kiribati, Kuwait, Kyrgyzstan, Laos, Latvia, Lebanon
            , Lesotho, Liberia, Libya, Liechtenstein, Lithuania, Luxembourg, Madagascar, Malawi, Malaysia, Maldives
            , Mali, Malta, MarshallIslands, Mauritania, Mauritius, Mexico, Micronesia, Moldova, Monaco, Mongolia
            , Montenegro, Morocco, Mozambique, Myanmar, Namibia, Nauru, Nepal, Netherlands, NewZealand, Nicaragua
            , Niger, Nigeria, NorthKorea, NorthMacedonia, Norway, Oman, Pakistan, Palau, Panama, PapuaNewGuinea
            , Paraguay, Peru, Philippines, Poland, Portugal, Qatar, Romania, Russia, Rwanda, SaintKittsandNevis
            , SaintLucia, SaintVincentandtheGrenadines, Samoa, SanMarino, SaoTomeandPrincipe, SaudiArabia
            , Senegal, Serbia, Seychelles, SierraLeone, Singapore, Slovakia, Slovenia, SolomonIslands, Somalia
            , SouthAfrica, SouthKorea, SouthSudan, Spain, SriLanka, Sudan, Suriname, Sweden, Switzerland, Syria
            , Tajikistan, Tanzania, Thailand, Togo, Tonga, TrinidadandTobago, Tunisia, Turkey, Turkmenistan
            , Tuvalu, Uganda, Ukraine, UnitedArabEmirates, UnitedKingdom, UnitedStatesofAmerica, Uruguay
            , Uzbekistan, Vanuatu, VaticanCity, Venezuela, Vietnam, Yemen, Zambia, Zimbabwe
    }
    public class Club:User
    {
        #region Properties
        [Required(ErrorMessage = "Club name is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Club name must be between 5 and 50!")]
        public string ClubName { get; set; }
        [Required(ErrorMessage = "Country is required!")]
        public Country Country{ get; set; }
        [Required(ErrorMessage = "City is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "City name must be between 5 and 50!")]
        public string City { get; set; }
        [Required(ErrorMessage = "League is required!")]
        public string League { get; set; }
        public string Website { get; set; }

        #endregion

        #region Constructors
        public Club()
        { 
        
        }
        public Club(string clubName,Country country,string city,string league,string website) 
        { 
         ClubName = clubName;
         Country = country;
         City = city;
         League = league;
         Website = website;
        }

        #endregion
    }
}
