using Microsoft.AspNetCore.Mvc;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using VividClub.Services;
using VividClub.Web.Infrastructure;
using VividClub.Web.Infrastructure.Filters;
using VividClub.Web.Models;
using VividClub.Web.Models.HomeViewModels;
using System;
using System.Diagnostics;
using System.Text;

namespace VividClub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService emailSender;

        public HomeController(IEmailService emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole(GlobalConstants.AdminRole))
            {
                return RedirectToAction("Index", "Home", new { area = GlobalConstants.AdminArea });
            }
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateRecaptcha]
        public IActionResult Contact(EmailModel model)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine($"From: {model.Name} {model.Email}");
            message.AppendLine();
            message.AppendLine(model.Message);

            try
            {
                this.emailSender.SendEmailAsync(GlobalConstants.VividClubEmail, model.Subject, message.ToString());
                ViewData["SuccessMessage"] = "Your email has been successfully sent!";
                return View(model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("EmailSenderror", "Something went wrong, please try again later!");
                return View(model);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}