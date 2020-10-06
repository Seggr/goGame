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
        const string emailValidationRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        static async Task Main(string[] args)
        {
            bool playerRegistered = false;

            while (!playerRegistered)
            {
                playerRegistered = await RegisterPlayer();
            }
        }

        static void LoginScreen()
        {
            Console.WriteLine("Welcome to the game of Go! Masters of Chess!\n\n\n");


        }


        static async Task<bool> RegisterPlayer()
        {
            string ServiceBusConnectionString = ConfigurationManager.AppSettings.Get("ServiceBusConnectionString");
            ManagementClient managementClient = new ManagementClient(ServiceBusConnectionString);
            string email;

            
            Console.WriteLine("Enter your email address to register (Ctrl+C to exit registration).");
            Console.Write("Email:");
            email = Console.ReadLine();


            if (Regex.IsMatch(email, emailValidationRegex, RegexOptions.IgnoreCase))
            {
                Console.WriteLine($"Hello World! Your email is {email}");

                email = email.Replace("@", "_");

                var currentQueues = await managementClient.GetQueuesAsync();

                if (currentQueues.Contains(new QueueDescription(email)))
                {
                    Console.WriteLine("Email already registered");
                    return false;
                }
                else
                {
                    await managementClient.CreateQueueAsync($"{email}");
                    return true;
                }
            }
            else
            {
                Console.WriteLine($"{email} is not a valid email address.");
                return false;
            }

        }
    }
}
