using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VividClub.Web.Infrastructure;

namespace VividClub.Web.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminArea)]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public abstract class AdminBaseController : Controller
    {
    }
}