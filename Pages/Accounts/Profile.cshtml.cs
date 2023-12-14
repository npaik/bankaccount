using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Repositories;
using WebApplication1.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Pages.Accounts
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ClientAccountRepo _accountRepo;

        public ClientAccountVM ClientAccount { get; set; }

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
            _accountRepo = new ClientAccountRepo(context);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login");
            }

            var userName = User.Identity.Name; 
         
            ClientAccount = await _accountRepo.GetClientAccountByEmail(userName);

            if (ClientAccount == null)
            {
                return NotFound("Account not found.");
            }

            return Page();
        }
    }
}
