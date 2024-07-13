using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public static class Start
{
    public static void Execute(string programFolder, string argument, List<Process> runningProcesses)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Specify an executable name or group to start.");
            return;
        }

        string[] executablesToStart;
        if (ExecutableGroups.Groups.TryGetValue(argument, out executablesToStart))
        {
            foreach (string exeFile in executablesToStart)
            {
                StartProcess(programFolder, exeFile, runningProcesses);
            }
        }
        else
        {
            StartProcess(programFolder, argument, runningProcesses);
        }
    }

    private static void StartProcess(string programFolder, string exeFile, List<Process> runningProcesses)
    {
        string fullPath = Path.Combine(programFolder, exeFile);

        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"Error: {exeFile} not found in {programFolder}.");
            return;
        }

        Console.WriteLine($"Starting {exeFile}...");

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = fullPath,
            WorkingDirectory = programFolder,
            UseShellExecute = true
        };

        Process process = Process.Start(startInfo);

        if (process == null)
        {
            Console.WriteLine($"Failed to start {exeFile}.");
            return;
        }

        runningProcesses.Add(process);

        while (!Program.IsProcessResponsive(process))
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{exeFile} is not yet responsive. Waiting...");
            Console.ResetColor();
            System.Threading.Thread.Sleep(1000);
        }
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{exeFile} started with PID {process.Id} and is responsive.");
        Console.ResetColor();
    }
}
