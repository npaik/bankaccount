using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Repositories;
using WebApplication1.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
     [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public string filteredValue { get; set; }

        public IList<ClientAccountVM> ClientAccountVM { get; set; } = default!;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            await LoadClientAccounts();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadClientAccounts();
            return Page();
        }

        private async Task LoadClientAccounts()
        {
            ClientAccountRepo clientAccountRepo = new ClientAccountRepo(_context);

            if (string.IsNullOrEmpty(filteredValue) || filteredValue == "All")
            {
                ClientAccountVM = await clientAccountRepo.All();
            }
            else
            {
                ClientAccountVM = await clientAccountRepo.filterUser(filteredValue);
            }
        }
    }
}