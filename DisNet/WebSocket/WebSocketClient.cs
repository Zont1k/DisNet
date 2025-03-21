using WebSocketSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DiscordSharpLib.Models;
using System.Threading;
using System;

namespace DiscordSharpLib.WebSocket
{
    public class WebSocketClient
    {
        private WebSocketSharp.WebSocket _ws; // Vollständiger Typ
        private readonly string _token;
        private int? _heartbeatInterval;
        private CancellationTokenSource _cts;

        public event EventHandler<Message> OnMessageReceived;

        public WebSocketClient(string token)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));

            _token = token;
            _ws = new WebSocketSharp.WebSocket("wss://gateway.discord.gg/?v=10&encoding=json"); // Vollständiger Typ
            _cts = new CancellationTokenSource();

            _ws.OnOpen += Ws_OnOpen;
            _ws.OnMessage += Ws_OnMessage;
            _ws.OnError += (sender, e) => Console.WriteLine($"WebSocket Error: {e.Message}");
            _ws.OnClose += (sender, e) => Console.WriteLine("WebSocket Closed");
        }

        public async Task ConnectAsync()
        {
            if (_ws == null) throw new InvalidOperationException("WebSocket не инициализирован");
            _ws.Connect();
            await StartHeartbeatAsync();
        }

        private void Ws_OnOpen(object sender, EventArgs e)
        {
            var payload = new
            {
                op = 2,
                d = new
                {
                    token = _token,
                    intents = 513,
                    properties = new
                    {
                        os = "windows",
                        browser = "DiscordSharpLib",
                        device = "DiscordSharpLib"
                    }
                }
            };
            _ws.Send(JsonConvert.SerializeObject(payload));
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(e.Data);
                int op = data.op;

                if (op == 10) // Hello event
                {
                    _heartbeatInterval = (int)data.d.heartbeat_interval;
                }
                else if (op == 0 && data.t == "MESSAGE_CREATE")
                {
                    var message = JsonConvert.DeserializeObject<Message>(e.Data.ToString());
                    OnMessageReceived?.Invoke(this, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки сообщения: {ex.Message}");
            }
        }

        private async Task StartHeartbeatAsync()
        {
            while (!_cts.IsCancellationRequested && _heartbeatInterval.HasValue)
            {
                await Task.Delay(_heartbeatInterval.Value, _cts.Token);
                _ws.Send(JsonConvert.SerializeObject(new { op = 1, d = (string)null }));
            }
        }

        public void Disconnect()
        {
            _cts.Cancel();
            _ws.Close();
        }
    }
}