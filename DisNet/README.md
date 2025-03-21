# DisNet
DisNet is an unofficial asynchronous, multi-platform .NET library for building Discord bots. It supports commands, WebSocket events, and REST API integration.

## Installation
```bash
dotnet add package DisNet

using DiscordSharpLib.Core;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new DiscordConfig { Token = "YOUR_TOKEN", Prefix = "!" };
        var client = new DiscordClient(config);

        client.RegisterCommand("ping", async (msg) => 
            await client.SendMessageAsync(msg.ChannelId, "Pong!"));

        await client.StartAsync();
        Console.ReadLine();
        client.Stop();
    }
}

Dependencies

WebSocketSharp
Newtonsoft.Json