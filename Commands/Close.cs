using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public static class Close
{
    public static void Execute(string argument, List<Process> runningProcesses)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Specify an executable name, group, or PID to close.");
            return;
        }

        if (ExecutableGroups.Groups.TryGetValue(argument, out var executablesToClose))
        {
            foreach (string exeFile in executablesToClose)
            {
                CloseProcess(exeFile, runningProcesses);
            }
        }
        else if (int.TryParse(argument, out int pid))
        {
            CloseProcess(pid, runningProcesses);
        }
        else
        {
            CloseProcess(argument, runningProcesses);
        }
    }

    private static void CloseProcess(string exeFile, List<Process> runningProcesses)
    {
        var process = runningProcesses.Find(p => p.ProcessName.Equals(Path.GetFileNameWithoutExtension(exeFile), StringComparison.OrdinalIgnoreCase));
        if (process != null)
        {
            process.Kill();
            process.WaitForExit();
            runningProcesses.Remove(process);
            Console.WriteLine($"{exeFile} with PID {process.Id} has been closed.");
        }
        else
        {
            Console.WriteLine($"Process {exeFile} is not running.");
        }
    }

    private static void CloseProcess(int pid, List<Process> runningProcesses)
    {
        var process = runningProcesses.Find(p => p.Id == pid);
        if (process != null)
        {
            process.Kill();
            process.WaitForExit();
            runningProcesses.Remove(process);
            Console.WriteLine($"Process with PID {pid} has been closed.");
        }
        else
        {
            Console.WriteLine($"No process found with PID {pid}.");
        }
    }
}
