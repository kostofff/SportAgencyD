using BusinessLayer;
using BusinessLayer.Entities;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contexts;

namespace SportAgencyDApplication.Controllers
{
    [Authorize(Roles = "Admin,Athlete,Club")]
    public class UsersController : Controller
    {
        private readonly UserIdentityContext usercontext;
        private readonly UserManager<User> _userManager;
        private readonly SportAgencyDbContext _sportAgencyDbContext;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserIdentityContext usercontext, UserManager<User> userManager, SportAgencyDbContext sportAgencyDbContext, SignInManager<User> signInManager)
        {
            this.usercontext = usercontext;
            _userManager = userManager;
            _sportAgencyDbContext = sportAgencyDbContext;
            _signInManager = signInManager;
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

            var viewModel = new UserDetailsViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserRole = user.UserRole.ToString(),
            };

            //Ако потребителя е атлет
            if (user is Athlete athlete)
            {
                viewModel.FirstName = athlete.FirstName;
                viewModel.LastName = athlete.LastName;
                viewModel.Age = athlete.Age;
            }
            //Ако потребителя е клуб
            else if (user is Club club)
            {
                viewModel.ClubName = club.ClubName;
                viewModel.Country = club.Country;
                viewModel.City = club.City;
                viewModel.League = club.League;
                viewModel.Website = club.Website;
            }

            return View(viewModel);
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



        // GET: AthleteAds/Edit/5
        [Authorize(Roles = "Admin,Athlete,Club")]
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
        [Authorize(Roles = "Admin,Athlete,Club")]
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
                    await usercontext.UpdateUserAsync(user.Id, user.UserName, user.Email, user.PhoneNumber);
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
                return RedirectToAction("Details", new { id = user.Id });
            }
            await LoadNavigationalProperties();
            return View(user);
        }


        #region Edit Athlete personal data

        // GET: Users/EditAthlete/5
        [HttpGet]
        [Authorize(Roles = "Admin,Athlete")]
        public async Task<IActionResult> EditAthlete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var athlete = user as Athlete;
            if (athlete == null) return BadRequest();

            var model = new EditAthleteViewModel
            {
                Id = athlete.Id,
                FirstName = athlete.FirstName,
                LastName = athlete.LastName,
                Age = athlete.Age
            };

            return View(model);
        }

        // POST: Users/EditAthlete/5
        [HttpPost]
        [Authorize(Roles = "Admin,Athlete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAthlete(EditAthleteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            var athlete = user as Athlete;
            if (athlete == null) return BadRequest();

            athlete.FirstName = model.FirstName;
            athlete.LastName = model.LastName;
            athlete.Age = model.Age;

            await _userManager.UpdateAsync(athlete);

            return RedirectToAction("Details", new { id = athlete.Id });
        }


        #endregion


        #region Edit Club personal data

        // GET: Users/EditClub/5
        [HttpGet]
        [Authorize(Roles = "Admin,Club")]
        public async Task<IActionResult> EditClub(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var club = user as Club;
            if (club == null) return BadRequest();

            var model = new EditClubViewModel
            {
                Id = club.Id,
                ClubName = club.ClubName,
                Country = club.Country,
                City = club.City,
                League = club.League,
                Website = club.Website
            };

            return View(model);
        }

        // POST: Users/EditClub/5
        [HttpPost]
        [Authorize(Roles = "Admin,Club")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClub(EditClubViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            var club = user as Club;
            if (club == null) return BadRequest();

            club.ClubName = model.ClubName;
            club.Country = model.Country;
            club.City = model.City;
            club.League = model.League;
            club.Website = model.Website;

            await _userManager.UpdateAsync(club);

            return RedirectToAction("Details", new { id = club.Id });
        }


        #endregion


        [Authorize(Roles = "Admin,Club,Athlete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            // Ако си Admin - може да трие всеки
            if (User.IsInRole("Admin"))
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Потребителят беше изтрит успешно.";
                    return RedirectToAction(nameof(Index)); 
                }
            }
            // Ако е обикновен потребител - може да трие само себе си
            else if (currentUser.UserName == username)
            {
                await _signInManager.SignOutAsync(); // излизане
                var result = await _userManager.DeleteAsync(currentUser);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Профилът ви беше успешно изтрит.";
                    return RedirectToAction("Index", "Home"); // след изтриване - към начална страница
                }
            }
            else
            {
                return Forbid(); // ако не е админ и не е собствения акаунт
            }

            TempData["ErrorMessage"] = "Грешка при изтриването на профила.";
            return RedirectToAction("Details", "User"); // ако стане грешка
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
