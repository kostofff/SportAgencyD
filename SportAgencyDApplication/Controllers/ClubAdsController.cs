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
    public class ClubAdsController : Controller
    {
        private readonly ClubAdContext _clubAdContext;
        private readonly UserIdentityContext _userIdentityContext;
        private readonly UserManager<User> _userManager;
        private readonly SportAgencyDbContext _context;

        public ClubAdsController(SportAgencyDbContext context,ClubAdContext clubAdContext,UserIdentityContext userIdentityContext,UserManager<User> userManager)
        {
            _clubAdContext = clubAdContext;
            _userIdentityContext = userIdentityContext;
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UserAds(string userId)
        {
            var userAds = _context.ClubAds
                                  .Where(a => a.UserId == userId)
                                  .ToList();

            return View(userAds);
        }


        [Authorize(Roles = "Athlete,Club,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetSortedClubAds(string sport, string position, string foot, string dateSort)
        {
            IQueryable<ClubAd> ads = _context.ClubAds;

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
                    ads = ads.Where(a => a.SearchedPosition == positionEnum);
                }
            }

            if (!string.IsNullOrEmpty(foot))
            {
                if (Enum.TryParse<LeftOrRightFoot>(foot, out var footEnum))
                {
                    ads = ads.Where(a => a.SearchedStrongFoot == footEnum);
                }
            }

            if (dateSort == "asc")
                ads = ads.OrderBy(ad => ad.CreatedAt);
            else
                ads = ads.OrderByDescending(ad => ad.CreatedAt);

            return PartialView("_ClubAdsPartial", await ads.ToListAsync());
        }





        // GET: ClubAds
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);//взима логнатия user
            var roles = await _userManager.GetRolesAsync(user);//взима ролята на логнатия user

            IQueryable<ClubAd> clubAds = _context.ClubAds.Include(c => c.User);

            if (roles.Contains("Club"))
            {
                // Ако потребителят е клуб, вижда само своите обяви
                clubAds = clubAds.Where(a => a.UserId == user.Id);
            }
            else if (roles.Contains("Athlete"))
            {
                // Ако потребителят е атлет, вижда всички обяви на атлети
                var clubs = await _userManager.GetUsersInRoleAsync("Club");
                var clubIds = clubs.Select(a => a.Id).ToList();

                clubAds = clubAds.Where(a => clubIds.Contains(a.UserId));
            }
            return View(await clubAds.ToListAsync());
        }

        // GET: ClubAds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubAd = await _clubAdContext.ReadAdAsync((string)id,true);
            if (clubAd == null)
            {
                return NotFound();
            }

            return View(clubAd);
        }

        // GET: ClubAds/Create
        [Authorize(Roles = "Club,Admin")]
        public async Task<IActionResult> Create()
        {
            await LoadNavigationalProperties();

            var userId = _userManager.GetUserId(User); // Взема ID на логнатия потребител

            var model = new ClubAd
            {
                UserId = userId // Автоматично присвояване на ID-то
            };

            ViewBag.Sports = new SelectList(Enum.GetValues(typeof(Sports)));
            ViewBag.SearchedStrongFoot = new SelectList(Enum.GetValues(typeof(LeftOrRightFoot)));


            return View(model); // Трябва да подадем `model`, за да останат стойностите попълнени!

        }

        // POST: ClubAds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Club,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Sport,SearchedPosition,SearchedStrongFoot,MinimumAge,MaximumAge,Description,CreatedAt,UserId")] ClubAd clubAd)
        {
            clubAd.UserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(clubAd.UserId))
            {
                Console.WriteLine("User ID is NULL!");
                return Unauthorized(); // Спира създаването, ако няма логнат потребител
            }

            if (ModelState.IsValid)
            {
                await _clubAdContext.CreateAdAsync(clubAd);//създаване обява
                Console.WriteLine("Ad Created Successfully!");
                return RedirectToAction(nameof(Index));
            }

            // Презареждане на dropdown списъците при невалиден ModelState
            await LoadNavigationalProperties();

            return View(clubAd);
        }

        // GET: ClubAds/Edit/5
        [Authorize(Roles ="Club,Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubAd = await _clubAdContext.ReadAdAsync((string)id);
            if (clubAd == null)
            {
                return NotFound();
            }

            ViewBag.Sports = new SelectList(Enum.GetValues(typeof(Sports)));
            ViewBag.SearchedStrongFoot = new SelectList(Enum.GetValues(typeof(LeftOrRightFoot)));

            await LoadNavigationalProperties();

            return View(clubAd);
        }

        // POST: ClubAds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Club,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Sport,SearchedPosition,SearchedStrongFoot,MinimumAge,MaximumAge,Description,CreatedAt,UserId")] ClubAd clubAd)
        {
            if (id != clubAd.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _clubAdContext.UpdateAdAsync(clubAd);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubAdExists(clubAd.Id))
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
            return View(clubAd);
        }

        // GET: ClubAds/Delete/5
        [Authorize(Roles = "Club,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubAd = await _clubAdContext.ReadAdAsync((string)id, true);
            if (clubAd == null)
            {
                return NotFound();
            }

            return View(clubAd);
        }

        // POST: ClubAds/Delete/5
        [Authorize(Roles = "Club,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _clubAdContext.DeleteAdAsync((string)id);
            return RedirectToAction(nameof(Index));
        }

        private bool ClubAdExists(string id)
        {
            return _clubAdContext.ReadAdAsync((string)id) is not null;
        }

        private async Task LoadNavigationalProperties()
        {
            ViewData["Users"] = new SelectList(await _userIdentityContext.ReadAllUsersAsync(), "Id", "Name");
        }
    }
}
