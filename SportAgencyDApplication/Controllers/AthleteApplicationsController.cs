using BusinessLayer.Entities;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAgencyDApplication.Services;
using static System.Net.Mime.MediaTypeNames;

namespace SportAgencyDApplication.Controllers
{
    public class AthleteApplicationsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SportAgencyDbContext _context;
        private readonly IApplicationService _applicationService;

        public AthleteApplicationsController(UserManager<User> userManager, SportAgencyDbContext context, IApplicationService applicationService)
        {
            _context = context;
            _userManager = userManager;
            _applicationService = applicationService;
        }

        [Authorize(Roles = "Athlete")]
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Apply(string id)
        {
            var userId = _userManager.GetUserId(User);
            var success = await _applicationService.ApplyAsync(id, userId);

            if (!success)
            {
                TempData["Error"] = "Вече сте кандидатствали или обявата не съществува.";
                return RedirectToAction("Details", "ClubAds", new { id });
            }

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
