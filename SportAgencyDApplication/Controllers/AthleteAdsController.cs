using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Entities;
using DataLayer;
using ServiceLayer.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SportAgencyDApplication.Controllers
{
    [Authorize(Roles = "Athlete,Club,Admin")] 
    public class AthleteAdsController : Controller
    {
        private readonly SportAgencyDbContext _context;
        private readonly UserIdentityContext usercontext;
        private readonly AthleteAdContext athleteAdContext;
        private readonly UserManager<User> _userManager;
        private readonly SportAgencyDbContext _sportAgencyDbContext;

        public AthleteAdsController(AthleteAdContext athleteAdContext, UserIdentityContext usercontext, UserManager<User> userManager, SportAgencyDbContext sportAgencyDbContext,SportAgencyDbContext context)
        {
            this.athleteAdContext = athleteAdContext;
            this.usercontext = usercontext;
            _userManager = userManager;
            _sportAgencyDbContext = sportAgencyDbContext;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UserAds(string userId)
        {
            var userAds = _context.AthleteAds
                                  .Where(a => a.UserId == userId)
                                  .ToList();

            return View(userAds);
        }



        [HttpGet]
        public async Task<IActionResult> GetSortedAthleteAds(string sport, string position, string country, string foot, string dateSort)
        {
            IQueryable<AthleteAd> ads = _context.AthleteAds;

            if (!string.IsNullOrEmpty(sport) && sport != "Всички")
            {
                if (Enum.TryParse<Sports>(sport, out var sportEnum))
                {
                    ads = ads.Where(a => a.Sport == sportEnum);
                }
            }

            if (!string.IsNullOrEmpty(position))
            {
                if (Enum.TryParse<Position>(position, out var positionEnum))
                {
                    ads = ads.Where(a => a.Position == positionEnum);
                }
            }

            if (!string.IsNullOrEmpty(country))
            {
                if (Enum.TryParse<Country>(country, out var countryEnum))
                {
                    ads = ads.Where(a => a.Country == countryEnum);
                }
            }

            if (!string.IsNullOrEmpty(foot))
            {
                if (Enum.TryParse<LeftOrRightFoot>(foot, out var footEnum))
                {
                    ads = ads.Where(a => a.LeftOrRighFoot == footEnum);
                }
            }

            if (dateSort == "asc")
                ads = ads.OrderBy(ad => ad.CreatedAt);
            else
                ads = ads.OrderByDescending(ad => ad.CreatedAt);

            return PartialView("_AthleteAdsPartial", await ads.ToListAsync());
        }









        // GET: AthleteAds
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);//взима логнатия user
            var roles = await _userManager.GetRolesAsync(user);//взима ролята на логнатия user

            IQueryable<AthleteAd> athleteAds = _sportAgencyDbContext.AthleteAds.Include(a => a.User);

            if (roles.Contains("Athlete"))
            {
                // Ако потребителят е атлет, вижда само своите обяви
                athleteAds = athleteAds.Where(a => a.UserId == user.Id);
            }
            else if (roles.Contains("Club"))
            {
                // Ако потребителят е клуб, вижда всички обяви на атлети
                var athletes = await _userManager.GetUsersInRoleAsync("Athlete");
                var athleteIds = athletes.Select(a => a.Id).ToList();

                athleteAds = athleteAds.Where(a => athleteIds.Contains(a.UserId));
            }

            return View(await athleteAds.ToListAsync());
        }

        // GET: AthleteAds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athleteAd = await athleteAdContext.ReadAdAsync((string)id,true);
            if (athleteAd == null)
            {
                return NotFound();
            }

            return View(athleteAd);
        }

        [HttpGet]
        //public IActionResult GetPositionsBySport(Sports sport)
        //{
        //    List<string> positions = sport switch
        //    {
        //        Sports.Football => Enum.GetNames(typeof(FootballPositions)).ToList(),
        //        Sports.Basketball => Enum.GetNames(typeof(BasketballPositions)).ToList(),
        //        Sports.Volleyball => Enum.GetNames(typeof(VolleyballPositions)).ToList(),
        //        _ => new List<string>()
        //    };

        //    return Json(positions);
        //}



        // GET: AthleteAds/Create
        [Authorize(Roles = "Athlete,Admin")]
        public async Task<IActionResult> Create()
        {
            await LoadNavigationalProperties();

            var userId = _userManager.GetUserId(User); // Взема ID на логнатия потребител

            var model = new AthleteAd
            {
                UserId = userId // Автоматично присвояване на ID-то
            };

            ViewBag.Sports = new SelectList(Enum.GetValues(typeof(Sports)));
            ViewBag.Country = new SelectList(Enum.GetValues(typeof(Country)));
            ViewBag.LeftOrRightFoot = new SelectList(Enum.GetValues(typeof(LeftOrRightFoot)));


            return View(model); // Трябва да подадем `model`, за да останат стойностите попълнени!

        }

        // POST: AthleteAds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Athlete,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Sport,Position,Country,City,LeftOrRighFoot,TeamsPlayed,Achievements,UserId")] AthleteAd athleteAd)
        {
            // Присвояване на UserId от логнатия потребител
            athleteAd.UserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(athleteAd.UserId))
            {
                Console.WriteLine("User ID is NULL!");
                return Unauthorized(); // Спира създаването, ако няма логнат потребител
            }

            if (ModelState.IsValid)
            {
                await athleteAdContext.CreateAdAsync(athleteAd);//създаване обява
                Console.WriteLine("Ad Created Successfully!");
                return RedirectToAction(nameof(Index));
            }

            // Презареждане на dropdown списъците при невалиден ModelState
            await LoadNavigationalProperties();

            return View(athleteAd);
        }

        // GET: AthleteAds/Edit/5
        [Authorize(Roles = "Athlete,Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athleteAd = await athleteAdContext.ReadAdAsync((string)id);
            if (athleteAd == null)
            {
                return NotFound();
            }

            ViewBag.Sports = new SelectList(Enum.GetValues(typeof(Sports)));
            ViewBag.Country = new SelectList(Enum.GetValues(typeof(Country)));
            ViewBag.LeftOrRightFoot = new SelectList(Enum.GetValues(typeof(LeftOrRightFoot)));
            await LoadNavigationalProperties();
            return View(athleteAd);
        }

        // POST: AthleteAds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Athlete,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Sport,Position,Country,City,LeftOrRighFoot,TeamsPlayed,Achievements,UserId")] AthleteAd athleteAd)
        {
            if (id != athleteAd.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await athleteAdContext.UpdateAdAsync(athleteAd);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AthleteAdExists(athleteAd.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await LoadNavigationalProperties();
            return View(athleteAd);
        }

        // GET: AthleteAds/Delete/5
        [Authorize(Roles = "Athlete,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athleteAd = await athleteAdContext.ReadAdAsync((string)id,true);
            if (athleteAd == null)
            {
                return NotFound();
            }

            return View(athleteAd);
        }

        // POST: AthleteAds/Delete/5
        [Authorize(Roles = "Athlete,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await athleteAdContext.DeleteAdAsync((string)id);
            return RedirectToAction(nameof(Index));
        }

        private bool AthleteAdExists(string id)
        {
            return athleteAdContext.ReadAdAsync((string)id) is not null;
        }

        private async Task LoadNavigationalProperties()
        {
            ViewData["Users"] = new SelectList(await usercontext.ReadAllUsersAsync(), "Id", "Name");
        }
    }
}
