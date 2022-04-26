using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnBinhMarket.Models.Components
{
    public class _DC_GiaoHangViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public _DC_GiaoHangViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
