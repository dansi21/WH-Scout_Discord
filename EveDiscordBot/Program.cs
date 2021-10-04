using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using ESI.NET;
using System.IO;
using EveDiscordBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using System.Net.Http;

namespace EveDiscordBot
{
    class Program
    {

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {

            var services = ConfigureServices();

            var _client = services.GetRequiredService<DiscordSocketClient>();

            //Set Logging
            _client.Log += Log;
            services.GetRequiredService<CommandService>().Log += Log;


            var token = File.ReadAllText("token.txt");


            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Here we initialize the logic required to register our commands.
            await services.GetRequiredService<CommandHandler>().InitializeAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage logMessage) {
            Console.WriteLine(logMessage.ToString());
            return Task.CompletedTask;
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<HttpClient>()
                .AddSingleton<EveESIService>()
                .BuildServiceProvider();
        }
    }
}
