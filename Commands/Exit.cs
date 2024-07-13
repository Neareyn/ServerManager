using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class Exit
{
    public static void Execute(List<Process> runningProcesses)
    {
        foreach (var process in runningProcesses)
        {
            if (!process.HasExited)
            {
                process.Kill();
                process.WaitForExit();
            }
        }
        runningProcesses.Clear();
        Console.WriteLine("All processes have been closed.");
    }
}
