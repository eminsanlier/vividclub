using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VividClub.Services;
using VividClub.Services.Models;
using VividClub.Web.Areas.Admin.Models.Users;
using VividClub.Web.Extensions;
using VividClub.Web.Infrastructure;
using VividClub.Web.Infrastructure.Filters;

namespace VividClub.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private const int PageSize = 3;

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index(int? page)
        {
            UserAccountModel user = this.userService.UserDetailsAndPosts(this.User.GetUserId(), page ?? 1, PageSize);

            //return this.ViewOrNotFound(user);
            ViewData[GlobalConstants.Authorization] = GlobalConstants.FullAuthorization;
            
            return this.View( "AccountDetails", user);
        }

        public IActionResult AccountDetails(string id, int? page)
        {
            //could be optimized
            if (User.GetUserId() == id)
            {
                ViewData[GlobalConstants.Authorization] = GlobalConstants.FullAuthorization;
            }
            else
            {
                ViewData[GlobalConstants.Authorization] = GlobalConstants.NoAuthorization;
            }

            UserAccountModel user = this.userService.UserDetails(id, page ?? 1, PageSize);

            return this.ViewOrNotFound(user);
        }

        public IActionResult Search(string searchTerm, int? page)
        {
            ViewData[GlobalConstants.SearchTerm] = searchTerm;

            if (string.IsNullOrEmpty(searchTerm))
            {
                var users = this.userService.All(page ?? 1, PageSize);
                return this.ViewOrNotFound(users);
            }
            else
            {
                var users = this.userService.UsersBySearchTerm(searchTerm, page ?? 1, PageSize);
                return this.ViewOrNotFound(users);
            }
        }

        public IActionResult Edit(string id)
        {
            if (!this.userService.UserExists(id))
            {
                return NotFound();
            }

            var userEditModel = Mapper.Map<UserEditModel>(this.userService.GetById(id));

            return View(userEditModel);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Edit(string id, UserEditModel model)
        {

            if (!this.userService.UserExists(id))
            {
                return NotFound();
            }

            if (this.userService.UserExistsByEmail(model.Email))
            {
                ModelState.AddModelError(string.Empty, "There is an active member with provided email!");
                return View(model);
            }

            this.userService.EditUser(id, model.FirstName, model.LastName, model.Email, model.Username, model.Gender, model.BirthDate);

            return RedirectToAction(nameof(HomeController.Index), "Home");
            //return View(model);
        }
    }
}