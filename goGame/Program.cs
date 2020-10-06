using System;
using System.Text.RegularExpressions;
using System.Configuration;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System.Threading.Tasks;
using System.Threading;

namespace goGame
{
    class Program
    {
        const string emailValidationRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        static async Task Main(string[] args)
        {

            await RegisterPlayer();

        
        }

        static async Task RegisterPlayer()
        {
            string ServiceBusConnectionString = ConfigurationManager.AppSettings.Get("ServiceBusConnectionString");
            ManagementClient managementClient = new ManagementClient(ServiceBusConnectionString);
            string email = "";


            for (; email.ToLower() != "q";)
            {
                Console.WriteLine("Welcome to the game of Go! Masters of Chess!\n\n\n");
                Console.WriteLine("Enter your email address to register (type 'q' to quit).");
                Console.Write("Email:");
                email = Console.ReadLine();

                if (Regex.IsMatch(email, emailValidationRegex, RegexOptions.IgnoreCase))
                {
                    email = email.Replace("@", "_");
                    var currentQueues = await managementClient.GetQueuesAsync();

                    if (currentQueues.Contains(new QueueDescription(email)))
                    {
                        Console.WriteLine("Email already registered.");
                    }
                    else
                    {
                        await managementClient.CreateQueueAsync($"{email}");
                        Console.WriteLine("New player registered.");
                        return;
                    }
                }
                else if (email.ToLower() == "q")
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"{email} is not a valid email address.");
                }
                Console.WriteLine("\n\n");
                Thread.Sleep(2000);
            }
        }
    }
}
