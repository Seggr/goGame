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
            string userInput = "";

            for (; userInput.ToLower() != "q";)
            {
                Console.WriteLine("Welcome to the game of Go! Masters of Chess!\n\n\n");

                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("Q. Quit");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        bool playerRegistered = false;
                        while (!playerRegistered)
                        {
                            playerRegistered = await RegisterPlayer();
                        }
                        break;
                    case "2":
                        await LoginPlayerMenu();
                        break;
                    case "Q":
                    case "q":
                        Console.WriteLine("Come again soon!");
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        Thread.Sleep(1000);
                        break;
                }
            }
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

        static async Task LoginPlayerMenu()
        {
            string ServiceBusConnectionString = ConfigurationManager.AppSettings.Get("ServiceBusConnectionString");
            ManagementClient managementClient = new ManagementClient(ServiceBusConnectionString);
            string email = "";

            for (; email.ToLower() != "q";)
            {
                Console.WriteLine("Enter your email address to login (type 'q' to quit).");
                Console.Write("Email:");
                email = Console.ReadLine();

                if (Regex.IsMatch(email, emailValidationRegex, RegexOptions.IgnoreCase))
                {
                    email = email.Replace("@", "_");

                    if (await managementClient.QueueExistsAsync(email))
                    {
                        Console.WriteLine("What would you like to do?");
                        Console.WriteLine("1. Start a new game");
                        Console.WriteLine("2. Resume a current game");
                        Console.WriteLine("Q. Quit");
                        email = Console.ReadLine();

                        switch (email)
                        {
                            case "1":
                                startANewGame();
                                break;
                            case "2":
                                Console.WriteLine("No games yet!");
                                break;
                            case "Q":
                            case "q":
                                Console.WriteLine("Come again soon!");
                                break;
                            default:
                                Console.WriteLine("Invalid input. Please try again.");
                                Thread.Sleep(1000);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Player isn't registered. Please try again.");
                    }
                }
                else if (email.ToLower() == "q")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid email address. Please try again.");
                }
                Console.WriteLine("\n\n");
                Thread.Sleep(1000);
            }
        }

        private static async Task startANewGame()
        {
            string ServiceBusConnectionString = ConfigurationManager.AppSettings.Get("ServiceBusConnectionString");
            ManagementClient managementClient = new ManagementClient(ServiceBusConnectionString);
            string email = "";

            for (; email.ToLower() != "q";)
            {
                Console.WriteLine("Enter email address of player you would like to play (type 'q' to quit).");
                Console.Write("Email:");
                email = Console.ReadLine();

                if (Regex.IsMatch(email, emailValidationRegex, RegexOptions.IgnoreCase))
                {
                    email = email.Replace("@", "_");
                    if(await managementClient.QueueExistsAsync(email))
                    {
                        Console.WriteLine("Request sent!");
                    }
                    else
                    {
                        Console.WriteLine("Player is not registered");
                    }

                }
                else if (email.ToLower() == "q")
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid email address. Please try again.");
                }
                Console.WriteLine("\n\n");
                Thread.Sleep(1000);
            }
        }


    }
}
