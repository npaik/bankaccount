using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Repositories;
using WebApplication1.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Data;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages.Accounts
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EditModel> _logger;
        [BindProperty]
        public ClientAccountVM ClientAccount { get; set; }

        public EditModel(ApplicationDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var repo = new ClientAccountRepo(_context);
            ClientAccount = await repo.GetClientAccountById(id);

            if (ClientAccount == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var repo = new ClientAccountRepo(_context);
            var success = await repo.UpdateClientAccount(ClientAccount);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "There was an error updating the account. Please try again.");
                return Page();
            }

            return RedirectToPage("./Detail", new { id = ClientAccount.ClientId });
        }

    }
}
