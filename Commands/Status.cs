using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;



public static class Status
{
    private static Dictionary<string, (bool IsOnline, int Pid)> previousStatuses = new Dictionary<string, (bool, int)>();
    private static CancellationTokenSource cancellationTokenSource;
    private static Task statusTask;

    public static void Execute(string programFolder, string argument)
    {
        if (string.IsNullOrWhiteSpace(argument) || argument == "start")
        {
            Start(programFolder);
        }
        else if(argument == "stop")
        {
            Stop();
        }
    }

    public static void Start(string programFolder)
    {
        if (statusTask != null && !statusTask.IsCompleted)
        {
            Console.WriteLine("Status monitoring is already running.");
            return;
        }

        previousStatuses = new Dictionary<string, (bool, int)>();

        cancellationTokenSource = new CancellationTokenSource();
        statusTask = Task.Run(() => Start(programFolder, cancellationTokenSource.Token), cancellationTokenSource.Token);
    }

    private static void Start(string programFolder, CancellationToken token)
    {
        if (!ExecutableGroups.Groups.TryGetValue("all", out var executablesToCheck)) // change "all" with the group you want to monitor, see ExecutabaleGroups.cs
        {
            Console.WriteLine("No executables found in the 'all' group.");
            return;
        }

        Console.Clear();
        Console.WriteLine("Checking statuses of executables...");

        while (!token.IsCancellationRequested)
        {
            bool hasChanges = false;
            var currentStatuses = new Dictionary<string, (bool IsOnline, int Pid)>();

            foreach (string exeFile in executablesToCheck)
            {
                string exeNameWithoutExtension = Path.GetFileNameWithoutExtension(exeFile);
                var process = Process.GetProcessesByName(exeNameWithoutExtension).FirstOrDefault();
                bool isOnline = process != null && !process.HasExited;
                int pid = isOnline ? process.Id : 0;

                currentStatuses[exeFile] = (isOnline, pid);

                if (!previousStatuses.TryGetValue(exeFile, out var previousStatus) || previousStatus != currentStatuses[exeFile])
                {
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                Console.SetCursorPosition(0, 1);
                Console.Clear();
                foreach (var exeFile in executablesToCheck)
                {
                    var status = currentStatuses[exeFile];

                    if (status.IsOnline)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{exeFile}: ONLINE (PID: {status.Pid})".PadRight(Console.WindowWidth - 1));
                        if (DiscordWebhook.AllowWebhook == true)
                        {
                            DiscordWebhook.SendMs($"{exeFile}: ONLINE", DiscordWebhook.WebhookURL2);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{exeFile}: OFFLINE".PadRight(Console.WindowWidth - 1));
                        if(DiscordWebhook.AllowWebhook == true && DiscordWebhook.UserID != "")
                        {
                            DiscordWebhook.SendMs($"{exeFile}: OFFLINE <@{DiscordWebhook.UserID}>", DiscordWebhook.WebhookURL);
                        }
                        else if(DiscordWebhook.AllowWebhook == true){
                            DiscordWebhook.SendMs($"{exeFile}: OFFLINE", DiscordWebhook.WebhookURL);
                        }
                        
                    }
                }
    
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter a command ('help' for a list of all commands):");
                Console.ResetColor();
                previousStatuses = new Dictionary<string, (bool, int)>(currentStatuses);
            }

            Thread.Sleep(2000);
        }

        // Clear the line when stopping
        Console.SetCursorPosition(0, 1);
        Console.Clear();
        Console.WriteLine("Status monitoring stopped.".PadRight(Console.WindowWidth - 1));
        
    }

    public static void Stop()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            statusTask.Wait();
            cancellationTokenSource = null;
            statusTask = null;
        }
        else
        {
            Console.WriteLine("Status monitoring is not running.");
        }
    }
}
