using System;
using System.Text.RegularExpressions;
using System.Configuration;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System.Threading.Tasks;

namespace goGame
{
	class Program
	{
		static async Task Main(string[] args)
		{
			const string ServiceBusConnectionString = "Endpoint=sb://fsx-sb-db.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=fzB4b39YYFr5jVDzxkFTH44RuiqbqGfPixWHGjJBNWw=";
			const string QueueName = "testqueue";
			IQueueClient queueClient;
			var managementClient = new ManagementClient(ServiceBusConnectionString);

			string value = ConfigurationManager.AppSettings.Get("Key0");


			const string emailValidationRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
			string email;

			Console.WriteLine("Welcome to the game of Go! Masters of Chess!\n\n\n");
			Console.WriteLine("Enter your email address to register.");
			Console.Write("Email:");
			email = Console.ReadLine();


			if (Regex.IsMatch(email, emailValidationRegex, RegexOptions.IgnoreCase))
			{
				Console.WriteLine($"Hello World! Your email is {email}");

				email = email.Replace("@", "_");
				await managementClient.CreateQueueAsync($"{email}");

				Console.WriteLine("Queue created!");


				//Task taskA = new Task(() => { });
				//taskA.Start();
				//taskA.Wait();
			}
			else
			{
				Console.WriteLine($"{email} is not a valid email address.");
			}

			Console.WriteLine(value);



		}
	}
}
