using System.Collections.Generic;

public static class ExecutableGroups
{
    public static readonly Dictionary<string, string[]> Groups = new Dictionary<string, string[]>
    {
        { "Channel 2", new[] { "AccountServer.exe", "Certifier.exe" } }, // I have no clue what is needed run a 2nd Channel, so feel free to adjust these.
        { "all", new[] { "AccountServer.exe", "Certifier.exe", "DatabaseServer.exe", "CoreServer.exe", "LoginServer.exe", "CacheServer.exe", "WorldServer.exe" } }
        // You can add more to be used with the "start" command
    };
}


// "all" is used for the monitor, you can add a "status" group and change it in Status.cs
// Same for the "start" command arguments -> Program.cs