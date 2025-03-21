using DiscordSharpLib.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordSharpLib.Core
{
    public class CommandHandler
    {
        private readonly string _prefix;
        private readonly Dictionary<string, Func<Message, Task>> _commands;

        public CommandHandler(string prefix)
        {
            _prefix = prefix;
            _commands = new Dictionary<string, Func<Message, Task>>();
        }

        public void RegisterCommand(string commandName, Func<Message, Task> action)
        {
            _commands[commandName.ToLower()] = action;
        }

        public async Task HandleMessageAsync(Message message)
        {
            if (!message.Content.StartsWith(_prefix)) return;

            var parts = message.Content.Substring(_prefix.Length).Split(' ');
            var commandName = parts[0].ToLower();

            if (_commands.ContainsKey(commandName))
            {
                await _commands[commandName](message);
            }
        }
    }
}