using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public static class Program
{
    static readonly List<Process> runningProcesses = new List<Process>();
    static readonly string programFolder = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        Dictionary<string, string[]> commandSequences = new Dictionary<string, string[]>
        {
            { "Default", new string[] { "start all" } }, // <- Just starts the executables
            { "Clean", new string[] { "start all", "clear" } }, // <- Starts the executables and clears the Command Prompt
            { "List", new string[] { "start all", "clear", "list" } }, // <-- Starts the executables then shows them all as list
            { "Monitor", new string[] { "start all", "clear", "status" } } // <-- Starts the executables and monitors them (+ activates webhooks if enabled in DiscordWebhook.cs
        };
        string selectedMode = "Clean"; 

       
        if (commandSequences.TryGetValue(selectedMode, out var commands))
        {
            foreach (var command in commands)
            {
                ExecuteCommand(command);
            }
        }
        else
        {
            Console.WriteLine($"Unknown mode: {selectedMode}");
        }

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter a command ('help' for a list of all commands):");
            Console.ResetColor();
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            ExecuteCommand(input);
        }
    }

    private static void ExecuteCommand(string input)
    {
        string[] parts = input.Split(' ', 2);
        string command = parts[0].ToLower();
        string argument = parts.Length > 1 ? parts[1] : string.Empty;

        switch (command)
        {
            case "start":
                Start.Execute(programFolder, argument, runningProcesses);
                break;

            case "close":
                Close.Execute(argument, runningProcesses);
                break;

            case "list":
                List.Execute(runningProcesses);
                break;

            case "clear":
                Clear.Execute();
                break;

            case "status":
                Status.Execute(programFolder, argument);
                break;


            case "exit":
                Exit.Execute(runningProcesses);
                return;

            case "help":
                    Help.Execute(argument);
                break;

            default:
                Console.WriteLine("Unknown command.");
                break;
        }


    }


    public static bool IsProcessResponsive(Process process)
        {
            if (process.HasExited)
                return false;

            try
            {
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
}







