using System;

namespace DiscordSharpLib.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public string ChannelId { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Discriminator { get; set; }
    }
}