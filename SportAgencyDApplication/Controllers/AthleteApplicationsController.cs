using BusinessLayer.Entities;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace SportAgencyDApplication.Controllers
{
    public class AthleteApplicationsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SportAgencyDbContext _context;

        public AthleteApplicationsController(UserManager<User> userManager, SportAgencyDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Athlete")]
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Apply(string id) // <-- получавай само ID-то на обявата
        {
            var userId = _userManager.GetUserId(User);

            var ad = await _context.ClubAds
                .FirstOrDefaultAsync(a => a.Id == id); // <-- зареждаш обявата от базата

            if (ad == null)
            {
                return NotFound();
            }

            var existingApplication = await _context.AthletesApplication
                .FirstOrDefaultAsync(a => a.AthleteId == userId && a.ClubAdId == ad.Id);

            if (existingApplication != null)
            {
                TempData["Error"] = "Вече сте кандидатствали за тази обява.";
                return RedirectToAction("Details", "ClubAds", new { id = ad.Id });
            }

            var application = new AthletesApplication
            {
                Status = ApplicationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                ClubId = ad.UserId,
                ClubAdId = ad.Id,
                AthleteId = userId
            };

            _context.AthletesApplication.Add(application);
            await _context.SaveChangesAsync();

            return RedirectToAction("ApplicationSuccess");
        }


        public IActionResult ApplicationSuccess()
        {
            return View();
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);//взима логнатия user
            var roles = await _userManager.GetRolesAsync(user);//взима ролята на логнатия user

            IQueryable<AthletesApplication> applications = _context.AthletesApplication
                .Include(a => a.ClubAd)
                .Include(a => a.Club)
                .Include(a => a.Athlete);

            if (roles.Contains("Club"))
            {
                applications = applications.Where(a => a.ClubId == user.Id);
            }
            else if (roles.Contains("Admin"))
            {
                applications = applications.Include(a => a.ClubAd).Include(a => a.Club).Include(a => a.Athlete);
            }

                return View(await applications.ToListAsync());
        }

        [HttpPost]
        public IActionResult ChangeStatus(string id, ApplicationStatus newStatus)
        {
            var application = _context.AthletesApplication.Find(id);
            if (application != null)
            {
                application.Status = newStatus;
                _context.SaveChanges();
            }
            return RedirectToAction("Details", "Users", new { id = application.AthleteId });

        }

    }
}
