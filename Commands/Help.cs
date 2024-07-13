using System;
using System.Collections.Generic;

public static class Help
{
    private static readonly Dictionary<string, string> HelpMessagesCommands = new Dictionary<string, string>
    {
        { "start", 
            "Usage: start <executable_name|group>\n" +
            "Example: start WorldServer.exe | start all | start Channel 2\n" +
            "Starts the specified executable or group of executables. See ExecutableGroups.cs to add more."},
        { "close", "Usage: close <executable_name|group|PID>\n" +
            "Closes the specified executable, group of executables, or process by PID. (use list or status to see the PIDs)" },
        { "list", "Usage: list\n" +
            "Lists all currently running processes started by this tool." },
        { "clear", "Usage: clear\n" +
            "Clears the console screen." },
        { "exit", "Usage: exit\n" +
            "Closes all running processes started by this tool and exits the application." },
        { "status", "Usage: status\n" +
            "Starts monitoring the status of all executables in the 'all' group.\n" +
            "Usage: status\n" +
            "Starts the monitoring.\n" +
            "Usage: status stop\n" +
            "Stops the monitoring." },
        { "help", "Usage: help\n" +
            "Displays this help message.\n" +
            "Usage: help <command>\n" +
            "Displays help for the specified command." },
    };

    private static readonly Dictionary<string, string> HelpMessagesGeneral = new Dictionary<string, string>
    {
        { "groups",
            "You can define all Executable Groups in ExecutableGroups.cs\n" +
            "Start.cs and Status.cs allows to use these predefined groups too."},
        { "webhook",
            "You can enable Webhooks for the 'status' command within DiscordWebhook.cs\n" +
            "The basic method allows to send a Discord message + ping when one of the executables are closed.\n" +
            "It can spam a lot (ex. when using the 'exit' command) so don't use it for public channels.\n" +
            "Example usage can be seen inside DiscordWebhook.cs and current usage is inside Status.cs"},
        { "whatsapp",
            "Not yet available\n" +
            "Similar to the Discord Webhooks, you'll receive a WhatsApp message when your server is down.\n" +
            "Requires a free WhatsApp Business Account. (1000 Messaged for free each month)"}
    };

    public static void Execute(string argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            DisplayGeneralHelp();
        }
        else if (HelpMessagesCommands.TryGetValue(argument.ToLower(), out var helpMessage))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(helpMessage);
            Console.ResetColor();
        }
        else if (HelpMessagesGeneral.TryGetValue(argument.ToLower(), out var helpMessage2))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(helpMessage2);
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Unknown command. Use 'help' to see all available commands.");
        }
    }

    private static void DisplayGeneralHelp()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Available commands:");
        Console.ForegroundColor = ConsoleColor.Cyan;
        foreach (var command in HelpMessagesCommands.Keys)
        {
            Console.WriteLine($"- {command}");
        }
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Other information:");
        Console.ForegroundColor = ConsoleColor.Cyan;
        foreach (var command in HelpMessagesGeneral.Keys)
        {
            Console.WriteLine($"- {command}");
        }
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Use 'help <command>' to get detailed help for a specific command.");
        Console.ResetColor();
    }
}
