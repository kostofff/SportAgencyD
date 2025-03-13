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
using BusinessLayer;

namespace SportAgencyDApplication.Controllers
{
    [Authorize(Roles = "Admin,Athlete,Club")]
    public class UsersController : Controller
    {
        private readonly UserIdentityContext usercontext;
        private readonly UserManager<User> _userManager;
        private readonly SportAgencyDbContext _sportAgencyDbContext;

        public UsersController(UserIdentityContext usercontext, UserManager<User> userManager, SportAgencyDbContext sportAgencyDbContext)
        {
            this.usercontext = usercontext;
            _userManager = userManager;
            _sportAgencyDbContext = sportAgencyDbContext;

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await usercontext.ReadAllUsersAsync());
        }

        [Authorize(Roles = "Admin,Athlete,Club")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await usercontext.ReadUserAsync((string)id, true);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // GET: AthleteAds/Create
        //public async Task<IActionResult> Create()
        //{
        //    await LoadNavigationalProperties();

        //    var userId = _userManager.GetUserId(User); // Взема ID на логнатия потребител

        //    var model = new AthleteAd
        //    {
        //        UserId = userId // Автоматично присвояване на ID-то
        //    };

        //    ViewBag.Sports = new SelectList(Enum.GetValues(typeof(Sports)));
        //    ViewBag.Country = new SelectList(Enum.GetValues(typeof(Country)));
        //    ViewBag.LeftOrRightFoot = new SelectList(Enum.GetValues(typeof(LeftOrRightFoot)));


        //    return View(model); // Трябва да подадем `model`, за да останат стойностите попълнени!

        //}




        // POST: AthleteAds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Athlete,Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Sport,Position,Country,City,LeftOrRighFoot,TeamsPlayed,Achievements,UserId")] AthleteAd athleteAd)
        //{
        //    // Присвояване на UserId от логнатия потребител
        //    athleteAd.UserId = _userManager.GetUserId(User);

        //    if (string.IsNullOrEmpty(athleteAd.UserId))
        //    {
        //        Console.WriteLine("User ID is NULL!");
        //        return Unauthorized(); // Спира създаването, ако няма логнат потребител
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        await athleteAdContext.CreateAdAsync(athleteAd);//създаване обява
        //        Console.WriteLine("Ad Created Successfully!");
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Презареждане на dropdown списъците при невалиден ModelState
        //    await LoadNavigationalProperties();

        //    return View(athleteAd);
        //}




        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await usercontext.ReadUserAsync((string)id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(Role)));
            await LoadNavigationalProperties();
            return View(user);
        }

        // POST: AthleteAds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PhoneNumber,UserRole")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await usercontext.UpdateUserAsync(user.Id,user.UserName,user.PhoneNumber,user.UserRole);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await usercontext.ReadUserAsync((string)id, true);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string username)
        {
            await usercontext.DeleteUserByNameAsync(username);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return usercontext.ReadUserAsync((string)id) is not null;
        }

        private async Task LoadNavigationalProperties()
        {
            ViewData["Users"] = new SelectList(await usercontext.ReadAllUsersAsync(), "Id", "Name");
        }
    }
}
