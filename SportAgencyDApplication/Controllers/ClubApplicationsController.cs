using BusinessLayer.Entities;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace SportAgencyDApplication.Controllers
{
    public class ClubApplicationsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SportAgencyDbContext _context;

        public ClubApplicationsController(UserManager<User> userManager, SportAgencyDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Club")]
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Apply(string id) // <-- получавай само ID-то на обявата
        {
            var userId = _userManager.GetUserId(User);

            var ad = await _context.AthleteAds
                .FirstOrDefaultAsync(a => a.Id == id); // <-- зареждаш обявата от базата

            if (ad == null)
            {
                return NotFound();
            }

            var existingApplication = await _context.ClubsApplication
                .FirstOrDefaultAsync(a => a.ClubId == userId && a.AthleteAdId == ad.Id);

            if (existingApplication != null)
            {
                TempData["Error"] = "Вече сте кандидатствали за тази обява.";
                return RedirectToAction("Details", "AthleteAds", new { id = ad.Id });
            }

            var application = new ClubsApplication
            {
                Status = ApplicationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                ClubId = userId,
                AthleteAdId = ad.Id,
                AthleteId = ad.UserId
            };

            _context.ClubsApplication.Add(application);
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

            IQueryable<ClubsApplication> applications = _context.ClubsApplication
                .Include(a => a.AthleteAd)
                .Include(a => a.Club)
                .Include(a => a.Athlete);

            if (roles.Contains("Athlete"))
            {
                applications = applications.Where(a => a.AthleteId == user.Id);
            }
            else if (roles.Contains("Admin"))
            {
                applications = applications.Include(a => a.AthleteAd).Include(a => a.Athlete).Include(a => a.Club);
            }

            return View(await applications.ToListAsync());
        }

        [HttpPost]
        public IActionResult ChangeStatus(string id, ApplicationStatus newStatus)
        {
            var application = _context.ClubsApplication.Find(id);
            if (application != null)
            {
                application.Status = newStatus;
                _context.SaveChanges();
            }
            return RedirectToAction("Details", "Users", new { id = application.ClubId });

        }

        [HttpPost]
        public IActionResult DeleteApp(string id)
        {
            var application = _context.ClubsApplication.Find(id);
            if (application != null)
            {
                _context.ClubsApplication.Remove(application);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
