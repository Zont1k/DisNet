using DiscordSharpLib.API;
using DiscordSharpLib.Models;
using DiscordSharpLib.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordSharpLib.Core
{
    public class DiscordClient
    {
        private readonly DiscordConfig _config;
        private readonly WebSocketClient _wsClient;
        private readonly DiscordHttpClient _httpClient;
        private readonly CommandHandler _commandHandler;

        public DiscordClient(DiscordConfig config)
        {
            _config = config;
            _wsClient = new WebSocketClient(config.Token);
            _httpClient = new DiscordHttpClient(config.Token);
            _commandHandler = new CommandHandler(config.Prefix);

            _wsClient.OnMessageReceived += async (sender, message) =>
            {
                if (message?.Content != null)
                {
                    await _commandHandler.HandleMessageAsync(message);
                }
            };
        }

        public async Task StartAsync()
        {
            await _wsClient.ConnectAsync();
        }

        public void RegisterCommand(string commandName, Func<Message, Task> action)
        {
            _commandHandler.RegisterCommand(commandName, action);
        }

        public async Task SendMessageAsync(string channelId, string content)
        {
            await _httpClient.SendMessageAsync(channelId, content);
        }

        public void Stop()
        {
            _wsClient.Disconnect();
        }
    }
}