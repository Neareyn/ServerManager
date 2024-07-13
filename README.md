# ServerManager
## Overview

ServerManager is a simple tool designed to manage and monitor multiple executable processes from a single command line interface. It provides functionality to start, stop, list, and monitor executables, as well as send notifications through Discord webhooks when certain events occur.
Intended to use for Flyff Servers but can also be used for other cases.


## Features

- Start Executables: Start individual executables or groups of executables defined in the `ExecutableGroups.cs`.
- Stop Executables: Stop individual executables, groups, or specific processes by their PID.
- List Running Processes: List all currently running processes started by this tool.
- Monitor Executables: Monitor the status of executables.
- Webhook Support: Send Discord Notifications of your executables status.
## Getting Started
## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed on your system.
## Running the Tool

1. Clone the Repository:

```
git clone https://github.com/yourusername/ServerStarter.git
cd ServerStarter
```

2. Build the Project:
```
dotnet build
```

3. Run the Project:
```
dotnet run
```
## Default Command Sequence

You can set a default command sequence to be executed when the tool starts by modifying the selectedMode variable in `Program.cs`. The available sequences are:

- Default: start all
- Clean: start all, clear
- List: start all, clear, list
- Monitor: start all, clear, status

You can add more command sequences as you need.
## Usage/Examples

![grafik](https://github.com/user-attachments/assets/962b2dfc-8820-4f05-93b1-8de591ff7623)



Once the tool is running, you can enter the following commands:

- Start an Executable:
```
start <executable_name|group>
```
Example: `start WorldServer.exe`, `start all`, `start Channel 2`

- Close an Executable:
```
close <executable_name|group|PID>
```
Example: `close WorldServer.exe`, `close all`, `close Channel 2`

- List Running Processes:
```
list
```
This also shows the PID of each Process.
- Clear the Console:
```
clear
```
- Monitor Executables:
```
status start
```
This also starts the Discord Webhook if it's enabled.
- Stop monitoring:
```
status stop
```
- Close all Executables:
```
exit
```
- Help:
```
help
```
- For specific command help:

```
help <command>
```
## Webhook Integration

To enable Discord webhook notifications, modify the `DiscordWebhook.cs` file:

- Set `AllowWebhook` to `true`.
- Set your `WebhookURL`.
- Set your `UserID` user pings.

Example:
```
public static bool AllowWebhook = true;
public static string WebhookURL = "your_webhook_url";
public static string UserID = "your_user_id";
```
Notifications by default are sent when an executable goes offline.
## Creating a Single Executable

To create a single executable file, you can publish the project using the following command:
```
dotnet publish -r win-x64 -c Release --self-contained
```
Replace `win-x64` with your target runtime identifier (RID). This will create a self-contained executable in the `bin\Release\net5.0\win-x64\publish\` directory.
## Using the Single Executable

1. Place the single executable in your Program folder.
2. Run the executable.
## Downloading Pre-built Executables

You can also download pre-built executables from the Releases section on [GitHub](https://github.com/Neareyn/ServerManager/releases).
## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.
