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
    public class DetailsModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;
        UserRoleRepo _userRepo = null;

        // Needed to access built-in methods of the Identity framework.
        private IServiceProvider _serviceProvider;

        public DetailsModel(WebApplication1.Data.ApplicationDbContext context,
                            IServiceProvider serviceProvider)
        {
            _context = context;
            _userRepo = new UserRoleRepo(serviceProvider);
            _serviceProvider = serviceProvider;
        }

        //  public RoleVM RoleVM { get; set; } = default!;
        public List<RoleVM> RoleVM { get; set; } = default!;
        public string requestedUser { get; set; } = "";

        public async Task<IActionResult> OnGetAsync(string id)
        {
            requestedUser = id;
            RoleVM = await _userRepo.GetUserRoles(id);
            return Page();
        }

    }
}
