using BusinessLayer.Entities;
using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contexts;
using DataLayer;
using Microsoft.AspNetCore.Mvc.Rendering;

public class CompleteClubProfileModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly SportAgencyDbContext _context;

    public CompleteClubProfileModel(UserManager<User> userManager, SportAgencyDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; }
    public List<SelectListItem> Countries { get; set; }

    public class InputModel
    {
        public string ClubName { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string League { get; set; }

        public string Website { get; set; }
    }

    public void OnGet()
    {
        // Зареждаме списък от държавите в падащото меню
        Countries = Enum.GetValues(typeof(Country))
                        .Cast<Country>()
                        .Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() })
                        .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            OnGet();
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        // Проверяваме дали потребителят е Club
        if (user.UserRole == Role.Club)
        {
            var club = user as Club;
            if (club != null)
            {
                club.ClubName = Input.ClubName;
                club.Country = Input.Country;
                club.City = Input.City;
                club.League = Input.League;
                club.Website = Input.Website;

                _context.Update(club);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToPage("/Index");
    }
}
