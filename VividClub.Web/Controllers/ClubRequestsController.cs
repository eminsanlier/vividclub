using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VividClub.Services;
using VividClub.Web.Extensions;

namespace VividClub.Web.Controllers
{
    [Authorize]
    public class ClubRequestsController : Controller
    {
        private readonly IClubRequestService clubRequestService;
        private readonly IUserService userService;

        public ClubRequestsController(IClubRequestService clubRequestService, IUserService userService)
        {
            this.clubRequestService = clubRequestService;
            this.userService = userService;
        }

        public IActionResult RequestClub(string senderId, string name)
        {
            if (!this.userService.UserExists(senderId) || string.IsNullOrEmpty(name) )
            {
                return NotFound();
            }

            this.clubRequestService.Create(senderId, name);

            return RedirectToAction("AccountDetails", "Users", new { id = name });
        }

        public IActionResult Accept(string senderId, string name)
        {
            this.clubRequestService.Accept(senderId, name);
            return RedirectToAction("AccountDetails", "Users", new { id = senderId });
        }

        public IActionResult Decline(string senderId, string name)
        {
            this.clubRequestService.Decline(senderId, name);
            return RedirectToAction("AccountDetails", "Users", new { id = senderId });
        }
    }
}