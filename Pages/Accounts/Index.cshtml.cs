using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Repositories;
using WebApplication1.ViewModels;

namespace WebApplication1.Pages.Accounts
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string filteredValue { get; set; }
        public IList<ClientAccountVM> ClientAccountVM { get; set; } = default!;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string filteredValue = "")
        {
            this.filteredValue = filteredValue;
            await LoadClientAccounts();
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