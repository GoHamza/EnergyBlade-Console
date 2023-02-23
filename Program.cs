using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using KrnlAPI;
using System.Diagnostics;
using System.Drawing;

namespace EnergyBlade
{
    internal class Program

    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();
        static void Main(string[] args)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            int length = 10; // set the length of the string

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                char c = chars[index];
                builder.Append(c);
            }

            SetWindowText(GetConsoleWindow(), builder.ToString());
            KrnlApi krnlApi = new KrnlApi();
            
            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleKeyInfo keyInfo;
            Console.WriteLine(@"

             ░▓▓▓▓▓▓▓▒▓▓▓▒                                                                
            ░▓▓▓▓▓▓░  ▒▓▓▒▒▓▓▓▓▒                                                          
           ░▓▓▓▓▓▓░            ▒▓▒▓                                                       
          ░▓▓▓▓▓▓                ▒▒▓▓                                                     
         ░▓▓▓▓▓▓            ▒▓▓     ▓▓▓▒                                                  
        ░▓▓▓▓▓▓      ▓▒▓▓▓▒▒▓▒▒▓▓▓     ▒▓▓░                                               
          ▒▓▒▒        ▒░         ▓░▓▒░   ▒▓▓▓      ░                                      
           ▓▓▓▓       ▒▓▓            ░▓░▓   ▓▓▓░░ ░░░░░                                   
             ▒▓░       ▒▓               ▓▓▓▓   ▓▓▓▓▒▓▓░                                   
              ░▒▓▓      ░▓░▒               ▓▓▓▒░░░░▓▒▒░░                                  
                ▓▓▓░      ▓▒▓▓▓               ▒▒▓▒░▒▓▓▓░                                  
                  ▓▒▓▓▒▓      ▓▓▓▒▓            ░░▓▓▓▓░ ▓▓▒                                
                      ▓▒▓▓▓▓      ▓▓▓▒         ░░░░▒░▒▓▓▒▓░▓▒                             
                           ▓▒▓▓▓▓    ░▒▓▒▓       ░ ░░░  ░▒▓▓▓▓▓▒                          
                                ▓░▓▓▒▒   ▒▒▓▒▓              ▓▓▓▓▓▓                        
                                     ▓░▓▓▓░  ▓▓▓▓░             ▓▓▓▒▓▓                     
                                        ░░▓░▓░▓ ▒▒▓░▓             ▒▒▓▓▓                   
                                     ░░░░▓░▒░ ░▓▓▓░▓░░▓▒▓            ░▓▓▓▓░               
                                       ░▒▓▒▓░░     ▒▒▓▓▒▓▓▓▓░            ▒▓▓▓             
                                     ░░░░▓▒░░░░         ▒▒▓▓▒▓▒▓             ▓▓▒          
                                        ░░ ░                 ▓▒▓▓▓▓▒            ▓▒        
                                            ░                     ▓░▓▓▓                   
                                                                      ░▓▓▒▓               
                                                                           ▒▒▓░           
                                                                                          
");
            Console.ForegroundColor = ConsoleColor.Blue;
            int currentWidth = Console.WindowWidth;
            int currentHeight = Console.WindowHeight;

            int targetWidth = 92;
            int targetHeight = 40;

            int steps = 25;
            int delay = 10; // 10 milliseconds delay between each step

            for (int i = 1; i <= steps; i++)
            {
                int newWidth = (int)Math.Round(currentWidth + (targetWidth - currentWidth) * ((double)i / steps));
                int newHeight = (int)Math.Round(currentHeight + (targetHeight - currentHeight) * ((double)i / steps));

                Console.SetWindowSize(newWidth, newHeight);
                System.Threading.Thread.Sleep(delay);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            string msg = "EnergyBlade by windowschips. Welcome, " + Environment.UserName + "!";
            int padding = (Console.WindowWidth - msg.Length) / 2;
            Console.WriteLine(msg.PadLeft(padding + msg.Length));
            string msg2 = "Time: " + DateTime.Now.ToString("hh:mm:ss tt");
            int padding2 = (Console.WindowWidth - msg2.Length) / 2;

            Console.WriteLine(msg2.PadLeft(padding2 + msg2.Length));
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n\n");
            Console.WriteLine("  Attempting to initialize...");

            if (!krnlApi.IsInitialized())
            {
                krnlApi.Initialize();
            } else
            {
                Console.WriteLine("Already initialized.");
            }
            while (!krnlApi.IsInitialized())
            {
                System.Threading.Thread.Sleep(100);
            }

            stopwatch.Stop();

            Console.WriteLine("  Done! Took ~{0} seconds. Press [ENTER] to load EnergyBlade or press any other key to exit.\n", stopwatch.ElapsedMilliseconds/1000);

            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {   
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length > 0)
                {
                    Stopwatch stopwatch2 = Stopwatch.StartNew();
                    Console.WriteLine("  Loading...");
                    if (!krnlApi.IsInjected())
                    {
                        krnlApi.Inject();
                    }
                    while (!krnlApi.IsInjected())
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    Console.WriteLine("  Finishing up...");
                    krnlApi.Execute("loadstring(game:HttpGet(\"https://github.com/GoHamza/EnergyBlade/blob/main/MainUI.lua?raw=true\"))");
                    stopwatch2.Stop();
                    Console.WriteLine("  All done! Took ~{0} seconds. Press any key to close this window.", stopwatch2.ElapsedMilliseconds/1000);
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  An error has occurred. Are you sure you have Roblox open?");
                    Console.WriteLine("  Please make sure Roblox is running, and then reopen EnergyBlade.");
                    Console.ReadKey();
                }
            }

        }
    }
}
