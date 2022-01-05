using JwtWebMVC.Jwt;
using JwtWebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JwtWebMVC.Controllers
{

    public class MembersController : Controller
    {
        private readonly IJwtAuth _jwtAuth;
        private readonly List<Member> lstMember = new List<Member>()
        {
            new Member{Id=1, Name="Omkar" },
            new Member {Id=2, Name="Yas" },
            new Member{Id=3, Name="Aditya"}
        };
        public MembersController(IJwtAuth jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }

        public IActionResult Index()
        {
            return View(lstMember);
        }

        [Authorize]
        [HttpGet]
        [Route("Show")]
        public IActionResult Show()
        {
            return Content("HTTP error 401- Unauthorized. It represents that the request could not be authenticated.");
        }

        [AllowAnonymous]
        [HttpPost("Authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = _jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                ViewBag.AccessToken = token;
            }
            return View();
        }
    }
}
