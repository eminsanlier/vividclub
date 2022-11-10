using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VividClub.Services;
using VividClub.Services.Models;
using VividClub.Web.Extensions;
using VividClub.Web.Infrastructure;

namespace VividClub.Web.Controllers
{
    [Authorize]
    public class ClubsController : Controller
    {
        private const int PageSize = 3;

        private readonly IClubService clubService;

        public ClubsController(IClubService clubService)
        {
            this.clubService = clubService;
        }

        public IActionResult Index(int? page)
        {
            var club = this.clubService.GetUserClubs(this.User.GetUserId(), page ?? 1, PageSize);

            return this.ViewOrNotFound(club);
        }

        public IActionResult Search(string searchTerm, int? page)
        {
            ViewData[GlobalConstants.SearchTerm] = searchTerm;

            if (string.IsNullOrEmpty(searchTerm))
            {
                var clubs = this.clubService.All(page ?? 1, PageSize);
                return View(clubs);
            }
            else
            {
                var clubs = this.clubService.ClubsBySearchTerm(searchTerm, page ?? 1, PageSize);
                return View(clubs);
            }
        }


        public IActionResult Join(int id)
        {
            if (!this.clubService.ClubExists(id))
            {
                return NotFound();
            }

            this.clubService.AddUserToClub(User.GetUserId(), id);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public IActionResult Details(int id)
        {
            if (!this.clubService.ClubExists(id))
            {
                return NotFound();
            }

            return this.ViewOrNotFound(this.clubService.Details(id));
        }

     
    }
}