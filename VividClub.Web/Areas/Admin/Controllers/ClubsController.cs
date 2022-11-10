using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Services;
using VividClub.Web.Areas.Admin.Models.Clubs;
using VividClub.Web.Infrastructure;
using VividClub.Web.Infrastructure.Filters;
using VividClub.Web.Models.AccountViewModels;

namespace VividClub.Web.Areas.Admin.Controllers
{
    public class ClubsController : AdminBaseController
    {
        private const int PageSize = 3;

        private readonly IClubService clubService;

        public static object Index()
        {
            throw new NotImplementedException();
        }

        public ClubsController(IClubService clubService)
        {
            this.clubService = clubService;
        }

        public IActionResult Search(string searchTerm, int? page)
        {
            ViewData[GlobalConstants.SearchTerm] = searchTerm;

            if (string.IsNullOrEmpty(searchTerm))
            {
                var users = this.clubService.All(page ?? 1, PageSize);
                return View(users);
            }
            else
            {
                var users = this.clubService.ClubsBySearchTerm(searchTerm, page ?? 1, PageSize);
                return View(users);
            }
        }

        public IActionResult Edit(int id)
        {
            if (!this.clubService.ClubExists(id))
            {
                return NotFound();
            }

            var clubEditModel = Mapper.Map<ClubEditModel>(this.clubService.GetById(id));

            return View(clubEditModel);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Edit(int id, ClubEditModel model)
        {
            if (!this.clubService.ClubExists(id))
            {
                return NotFound();
            }

            this.clubService.EditClub(id, model.Name, model.Description, model.Photo);

            return RedirectToAction(nameof(Search), new { searchTerm = model.Name });
        }

        public IActionResult Delete(int id)
        {
            if (!this.clubService.ClubExists(id))
            {
                return NotFound();
            }

            var clubEditModel = Mapper.Map<ClubEditModel>(this.clubService.GetById(id));

            return View(clubEditModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Destroy(int id)
        {
            if (!this.clubService.ClubExists(id))
            {
                return NotFound();
            }


            this.clubService.DeleteClub(id);

            return RedirectToAction(nameof(Search));
        }




        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClubCreateModel model, string returnUrl = null)
        {
            if (model.Photo != null)
            {
                if (model.Photo.Length > DataConstants.MaxPhotoLength)
                {
                    ModelState.AddModelError(string.Empty, "Your photo should be a valid image file with max size 5MB!");
                    return View(model);
                }
            }
            

            if (!string.IsNullOrEmpty(model.Name))
            {
                var clubs = this.clubService.ClubsBySearchTerm(model.Name, 1, PageSize);
                if (clubs.Count != 0)
                {
                    ModelState.AddModelError(string.Empty, "Club name must be unique.");
                    return View(model);
                }
            }

            try
            {
                this.clubService.CreateClub(model.Name, model.Description, model.Photo);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "There was an error while creating a club");
                return View(model);
            }
            

            return RedirectToAction(nameof(Search));
        }
    }
}
