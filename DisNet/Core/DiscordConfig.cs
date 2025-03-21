namespace DiscordSharpLib.Core
{
    public class DiscordConfig
    {
        public string Token { get; set; }
        public string Prefix { get; set; } = "!";
        public int Intents { get; set; } = 513; // GUILDS + GUILD_MESSAGES
    }
}