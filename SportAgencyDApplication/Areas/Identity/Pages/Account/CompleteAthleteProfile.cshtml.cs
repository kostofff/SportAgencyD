using BusinessLayer.Entities;
using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contexts;
using DataLayer;
using System.ComponentModel.DataAnnotations;

public class CompleteAthleteProfileModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly SportAgencyDbContext _context;

    public CompleteAthleteProfileModel(UserManager<User> userManager, SportAgencyDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        // Get the current user
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound("User not found.");
        }
        // Check if the user is an athlete
        if (user.UserRole == Role.Athlete)
        {
            // Update the athlete's profile
            var athlete = user as Athlete;
            if (athlete != null)
            {
                athlete.FirstName = Input.FirstName;
                athlete.LastName = Input.LastName;
                athlete.Age = Input.Age;

                _context.Update(athlete);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToPage("/Index");
    }
}
