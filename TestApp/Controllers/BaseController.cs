using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestApp.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected string CurrentUserId()
        {
            return HttpContext.User.FindFirstValue("Id");

        }

    }

}
