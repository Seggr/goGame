using System;
using System.Text.RegularExpressions;
using System.Configuration;

 
namespace goGame
{
	class Program
	{
		static void Main(string[] args)
		{
			string value = ConfigurationManager.AppSettings.Get("Key0");


			const string emailValidationRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
			string email;

			Console.WriteLine("Welcome to the game of Go! Masters of Chess!\n\n\n");
			Console.WriteLine("Enter your email address to register.");
			Console.Write("Email:");
			email = Console.ReadLine();


			if(Regex.IsMatch(email, emailValidationRegex, RegexOptions.IgnoreCase))
			{
				Console.WriteLine($"Hello World! Your email is {email}");
			}
			else
			{
				Console.WriteLine($"{email} is not a valid email address.");
			}

			Console.WriteLine(value);

		}
	}
}
