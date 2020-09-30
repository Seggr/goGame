using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace goGame.Controllers
{
	public class RegistrationController : Controller
	{
		const string emailValidationExpression = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

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


			if (Regex.IsMatch(email, emailValidationExpression,RegexOptions.IgnoreCase))
			{
				ViewData["validEmail"] = true;

			}
			else
			{
				ViewData["validEmail"] = false;
			}
			return View();

		}
	}
}
