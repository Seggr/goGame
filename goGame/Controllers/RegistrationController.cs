using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace goGame.Controllers
{
	public class RegistrationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View("Index");
		}

		[HttpPost]
		public IActionResult Register(string email)
		{
			ViewData["registeredEmail"] = email;
			return View();
		}
	}
}
