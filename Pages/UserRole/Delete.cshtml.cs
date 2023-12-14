using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Repositories;
using WebApplication1.ViewModels;

namespace WebApplication1.Pages.UserRole
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;

        public DeleteModel(WebApplication1.Data.ApplicationDbContext context,
                IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        [BindProperty]
        public UserRoleVM userRoleVM { get; set; } = new UserRoleVM();
        public List<UserVM> userVMs { get; set; }
        public List<RoleVM> roleVMs { get; set; }
        public string errorMessage = "";

        public async Task<IActionResult> OnGetAsync(string Email, string Role)

        {

            userRoleVM.Email = Email;
            userRoleVM.Role = Role;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            UserRepo userRepo = new UserRepo(this._context);
            userVMs = await userRepo.All();



            if (!ModelState.IsValid)
            { // Server side check.
                errorMessage = "Please ensure that an option from each dropdown is selected and try again.";
                return Page();
            }

            UserRoleRepo roleRepo = new UserRoleRepo(this._serviceProvider);
            var result = await roleRepo.RemoveUserRole(userRoleVM.Email, userRoleVM.Role);

            return RedirectToPage("./Index");
        }
    }
}
