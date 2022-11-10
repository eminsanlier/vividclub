using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VividClub.Services;
using VividClub.Web.Extensions;
using VividClub.Web.Infrastructure;
using VividClub.Web.Infrastructure.Filters;
using VividClub.Web.Models.Messanger;

namespace VividClub.Web.Controllers
{
    [Authorize]
    public class MessangerController : Controller
    {
        private const int PageSize = 10;

        private readonly IUserService userService;
        private readonly IMessangerService messangerService;

        public MessangerController(IUserService userService, IMessangerService messangerService)
        {
            this.userService = userService;
            this.messangerService = messangerService;
        }

        public IActionResult Index(string id, int? pageIndex)
        {
            if (id == this.User.GetUserId() )
            {
                return BadRequest();
            }

            var messangerModel = new MessangerModel();

            string compositeChatId = string.Compare(User.GetUserId(), id) > 0 ? User.GetUserId() + id : id + User.GetUserId();

            ViewData[GlobalConstants.CompsiteChatId] = compositeChatId;
            ViewData[GlobalConstants.CounterPartFullName] = this.userService.GetUserFullName(id);

            messangerModel.Messages = this.messangerService.AllByUserIds(User.GetUserId(), id, pageIndex ?? 1, PageSize);

            return this.ViewOrNotFound(messangerModel);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Index(string id, int? pageIndex, MessangerModel model)
        {
            if (id == this.User.GetUserId())
            {
                return BadRequest();
            }

            this.messangerService.Create(User.GetUserId(), id, model.MessageText);
            return RedirectToAction(nameof(Index), new { id, pageIndex });
        }
    }
}