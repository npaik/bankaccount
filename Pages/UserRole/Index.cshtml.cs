using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Repositories;
using WebApplication1.ViewModels;

namespace WebApplication1.Pages.UserRole
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public IndexModel(WebApplication1.Data.ApplicationDbContext context)
        { _context = context; }

        public List<UserVM> UserVM { get; set; } = default!;

        public async Task OnGetAsync()
        {
            UserRepo userRepo = new UserRepo(_context);
            UserVM = await userRepo.All();
        }

    }
}
