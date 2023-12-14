using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Repositories;
using WebApplication1.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Pages.Accounts
{
    [Authorize(Roles = "Admin")]
    public class DetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ClientAccountVM ClientAccount { get; set; }

        public DetailModel(ApplicationDbContext context)
        {
            _context = context;
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
    }
}