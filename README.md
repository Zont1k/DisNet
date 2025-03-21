# DisNet

![GitHub release (latest by date)](https://img.shields.io/github/v/release/Zont1k/DisNet?style=flat-square)
![NuGet](https://img.shields.io/nuget/v/DisNet?style=flat-square)
![GitHub license](https://img.shields.io/github/license/Zont1k/DisNet?style=flat-square)
![GitHub issues](https://img.shields.io/github/issues/Zont1k/DisNet?style=flat-square)
![GitHub stars](https://img.shields.io/github/stars/Zont1k/DisNet?style=flat-square)

**DisNet** is an unofficial asynchronous .NET library for building Discord bots. It provides a simple and efficient way to interact with the Discord API, supporting commands, WebSocket events, and REST API integration.

---

## ✨ Key Features

- 🚀 **Asynchronous API**: Built with `async/await` for high performance.
- 🤖 **Command System**: Easily register and handle commands with a prefix.
- 🌐 **WebSocket Support**: Real-time event handling via Discord Gateway.
- 📡 **REST API Integration**: Send messages and interact with Discord via HTTP.
- 🛠️ **Cross-Platform**: Supports .NET 6.0 and above.

---

## 📦 Installation

Install DisNet via NuGet by running the following command:

```bash
dotnet add package DisNet
```

Alternatively, you can find DisNet in the NuGet Package Manager in Visual Studio.

🚀 Quick Start
Follow these steps to get your Discord bot up and running with DisNet:

1. Create a Discord Bot
Go to the Discord Developer Portal.
Create a new application and add a bot.
Copy the bot token and enable the necessary intents (e.g., GUILDS, GUILD_MESSAGES).
2. Install DisNet
Add the package to your project using the command above.

3. Write Your Code
Here’s a simple example of a bot that responds to the !ping command:

```bash
using DiscordSharpLib.Core;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new DiscordConfig
        {
            Token = "YOUR_BOT_TOKEN_HERE", // Replace with your bot token
            Prefix = "!"
        };

        var client = new DiscordClient(config);

        client.RegisterCommand("ping", async (message) =>
        {
            await client.SendMessageAsync(message.ChannelId, "Pong! 🏓");
            Console.WriteLine($"Received !ping command from {message.Author.Username}");
        });

        await client.StartAsync();

        Console.WriteLine("Bot is running! Press Enter to exit...");
        Console.ReadLine();
        client.Stop();
    }
}
```

4. Run Your Bot
Run your project, and the bot will connect to Discord. Try sending !ping in a channel where the bot has access.

📖 Documentation
Here’s a brief overview of the main components:

DiscordClient: The main class for interacting with the Discord API.
DiscordConfig: Configuration for the bot (token, prefix).
RegisterCommand: Method to register commands.
SendMessageAsync: Method to send messages to a channel.
Detailed documentation will be added in the future. Stay tuned for updates! 📚

🛠️ Dependencies
DisNet relies on the following libraries:

WebSocketSharp (1.0.3-rc11)
Newtonsoft.Json (13.0.3)

These dependencies are automatically installed when you add DisNet via NuGet.
