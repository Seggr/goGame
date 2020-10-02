using System;

namespace goGame
{
	class Program
	{
		static void Main(string[] args)
		{
			string email;

			Console.WriteLine("Welcome to the game of Go! Masters of Chess!\n\n\n");
			Console.WriteLine("Enter your email address to register.");
			Console.Write("Email:");
			email = Console.ReadLine();


			// ^(?(")(".+?(?<!\\)"@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$



			Console.WriteLine($"Hello World! Your email is {email}");
		}
	}
}
