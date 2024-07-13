using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class List
{
    public static void Execute(List<Process> runningProcesses)
    {
        var runningProcessesCopy = new List<Process>(runningProcesses);

        foreach (var process in runningProcessesCopy)
        {
            if (process.HasExited)
            {
                runningProcesses.Remove(process);
            }
        }

        if (runningProcesses.Count == 0)
        {
            Console.WriteLine("No processes are currently running.");
            return;
        }

        Console.WriteLine("Running processes:");
        foreach (var process in runningProcesses)
        {
            Console.WriteLine($"- {process.ProcessName} (PID: {process.Id})");
        }
    }
}
